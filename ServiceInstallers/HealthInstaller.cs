using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    /// <summary>
    /// Setups health check end points
    /// </summary>
    public class HealthInstallerL : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            services.AddHealthEndpoints();
        }
    }
}
