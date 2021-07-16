using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    /// <summary>
    /// Any installer inheriting from this interface will be installed to both API and Background services 
    /// These services will be added at startup
    /// </summary>
    public interface IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup);
    }
}
