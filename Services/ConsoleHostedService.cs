using Framework.Annotations;
using Framework.Extensions;
using Framework.Helpers;
using Framework.Interfaces;
using Framework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class ConsoleHostedService<TContext, IState, TState, TStartup> : BackgroundService
        where TContext : DbContext
    {
        private readonly IRabbitMqService rabbitMqService;
        private readonly IConfiguration configuration;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<ConsoleHostedService<TContext, IState, TState, TStartup>> logger;
        private IState state { get; set; }

        public ConsoleHostedService(IRabbitMqService rabbitMqService, IConfiguration configuration,
            ILogger<ConsoleHostedService<TContext, IState, TState, TStartup>> logger, IServiceProvider serviceProvider, IState state)
        {
            this.rabbitMqService = rabbitMqService;
            this.configuration = configuration;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.state = state;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var requestServicesTypes = TypeHelper.GetAssignableTypes(typeof(IConsoleRequestService));
            requestServicesTypes.ForEach(rst => ReceiveRequest(rst));
            return Task.CompletedTask;
        }

        private void ReceiveRequest(Type requestServiceType)
        {
            var methods = requestServiceType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                   .Where(mi => mi.GetCustomAttributes<BackgroundRequest>().Any()).ToList();
            methods.ForEach(method =>
            {
                var queueAttr = method.GetCustomAttribute<BackgroundRequest>();
                var isAwaitable = method.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null;
                rabbitMqService.ReceiveRequest(queueAttr.Queue, queueAttr.TypeOfRequest,
                    (Func<object, TState, ConsoleRequestMeta, Task<bool>>)(async (data, state, meta) =>
                        await HandleRequest(requestServiceType, method, data, state, isAwaitable)));
            });
        }

        private async Task<bool> HandleRequest(Type requestServiceType, MethodInfo method, object data, TState state, bool isAwaitable)
        {
            try
            {
                state.MapEntity(this.state);
                using (var scope = serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService(requestServiceType);
                    if (isAwaitable)
                    {
                        var task = (Task)method.Invoke(service, new object[] { data });
                        await task.ConfigureAwait(true);
                        if (task.Status == TaskStatus.Faulted)
                            throw task.Exception.InnerException;
                    }
                    else
                    {
                        method.Invoke(service, new object[] { data });
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to complete request due to unknown exception");
                return false;
            }
            return true;
        }
    }
}