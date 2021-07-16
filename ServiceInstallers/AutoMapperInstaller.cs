using Framework.Helper;
using Framework.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            services.AddAutoMapper(config => TypeHelper.GetAssignableTypes(typeof(IMapperProfile)).ForEach(profileType => config.AddProfile(profileType)));
        }
    }
}
