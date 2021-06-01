using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public class AutoMapperInstaller : IAutoMapperInstaller
    {
        public void InstallServices(IServiceCollection services, Type mapperProfileType)
        {
            services.AddAutoMapper(mapperProfileType);
        }
    }
}
