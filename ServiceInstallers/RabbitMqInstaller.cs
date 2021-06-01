using Framework.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.ServiceInstallers
{
    public class RabbitMqInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqService = new RabbitMqService(
                configuration["RabbitMqConfiguration:Uri"],
                int.Parse(configuration["RabbitMqConfiguration:Port"]),
                configuration["RabbitMqConfiguration:Username"],
                configuration["RabbitMqConfiguration:Password"]);
            services.AddSingleton(rabbitMqService);
        }
    }
}
