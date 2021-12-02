using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public class LoggingInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            services.AddLogging();
        }
    }
}