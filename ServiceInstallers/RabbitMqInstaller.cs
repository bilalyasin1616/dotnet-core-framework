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
            var connection = new ConnectionFactory()
            {
                HostName = configuration["RabbitMqConfiguration:Uri"],
                Port = int.Parse(configuration["RabbitMqConfiguration:Port"]),
                UserName = configuration["RabbitMqConfiguration:Username"],
                Password = configuration["RabbitMqConfiguration:Password"],
            };
            services.AddSingleton(connection);
            services.AddScoped<IRabbitMqService, RabbitMqService>();
        }
    }
}
