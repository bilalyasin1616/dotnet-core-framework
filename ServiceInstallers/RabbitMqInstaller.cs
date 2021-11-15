using Framework.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;

namespace Framework.ServiceInstallers
{
    public class RabbitMqInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            if (configuration["RabbitMq:Enabled"]!=null && bool.Parse(configuration["RabbitMq:Enabled"]) && configuration["RabbitMq:Uri"] != null)
            {
                var connection = new ConnectionFactory()
                {
                    HostName = configuration["RabbitMq:Uri"],
                    Port = int.Parse(configuration["RabbitMq:Port"]),
                    UserName = configuration["RabbitMq:Username"],
                    Password = configuration["RabbitMq:Password"],
                };
                services.AddSingleton(connection);
                services.AddScoped<IRabbitMqService, RabbitMqService>();
            }
        }
    }
}
