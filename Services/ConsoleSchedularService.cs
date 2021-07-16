using Framework.Annotations;
using Framework.Helper;
using Framework.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    public class ConsoleSchedularService<TContext, TStartup> : BackgroundService
        where TContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<ConsoleSchedularService<TContext, TStartup>> logger;
        public ConsoleSchedularService(ILogger<ConsoleSchedularService<TContext, TStartup>> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var schedularServiceTypes = TypeHelper.GetAssignableTypes(typeof(IConsoleSchedular));
            schedularServiceTypes.ForEach(sst => ScheduleTask(sst));
            return Task.CompletedTask;
        }

        private void ScheduleTask(Type schedularServiceType)
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        using (var scope = serviceProvider.CreateScope())
                        {
                            var scheduler = (IConsoleSchedular)scope.ServiceProvider.GetRequiredService(schedularServiceType);
                            await scheduler.RunScheduledTask();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogTrace(ex, "Schedular stopped due to unknown error");
                    }
                    if (schedularServiceType.GetCustomAttributes<BackgroundSchedular>().Any())
                        Thread.Sleep((int)TimeSpan.FromMinutes(schedularServiceType.GetCustomAttribute<BackgroundSchedular>().ScheduledTimeMinute).TotalMilliseconds);
                    else
                        Thread.Sleep(TimeSpan.FromMinutes(30));
                }
            });
        }
    }
}
