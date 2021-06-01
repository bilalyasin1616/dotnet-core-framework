using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public interface IAutoMapperInstaller
    {
        public void InstallServices(IServiceCollection services, Type mapperProfileType);
    }
}
