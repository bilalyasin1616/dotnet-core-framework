using Framework.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public class SchedulerInstallers : IConsoleInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            services.InstallServicesFromInterface(typeof(IConsoleSchedular));
        }
    }
}
