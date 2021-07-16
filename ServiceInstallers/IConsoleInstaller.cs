using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    /// <summary>
    /// These services will be installed to console background service requests in a hosted service
    /// Beacuse hosted services are initilized on startup we can't dynamically create objects of service classes for receving request
    /// So we have to configure another DI container for background requests
    /// </summary>
    public interface IConsoleInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup);
    }
}
