using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public interface IPostgressInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup);
    }
}