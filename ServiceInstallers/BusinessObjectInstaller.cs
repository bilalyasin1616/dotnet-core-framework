using Framework.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    /// <summary>
    /// Installs business objects from respositories for DI
    /// </summary>
    public class BusinessObjectInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            services.InstallServicesFromInterface(typeof(IBusinessObject));
        }
    }
}
