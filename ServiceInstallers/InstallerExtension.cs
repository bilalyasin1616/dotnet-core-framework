using Framework.Helper;
using Framework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public static class InstallerExtension
    {
        public static void InstallApiServices<C>(this IServiceCollection services, IConfiguration configuration, Type mapperProfileType) where C : DbContext
        {
            TypeHelper.CreateObjects<IInstaller>().ForEach(rc => rc.InstallServices(services, configuration));
            TypeHelper.CreateObjects<IAutoMapperInstaller>().ForEach(rc => rc.InstallServices(services, mapperProfileType));
            new DataInstaller<C>().InstallServices(services, configuration);
        }

        public static void InstallConsoleRequestServices<C>(this IServiceCollection services, IConfiguration configuration, Type mapperProfileType) where C : DbContext
        {
            TypeHelper.CreateObjects<IInstaller>().ForEach(installer =>
            {
                var installerType = installer.GetType();
                if (installerType != typeof(RabbitMqInstaller) && installerType != typeof(SerilogInstaller))
                    installer.InstallServices(services, configuration);
            });
            TypeHelper.CreateObjects<IAutoMapperInstaller>().ForEach(rc => rc.InstallServices(services, mapperProfileType));
            services.InstallServicesByInterface(typeof(IConsoleRequestService));
            new DataInstaller<C>().InstallServices(services, configuration);
        }

        public static void InstallConsoleStartupServices(this IServiceCollection services, IConfiguration configuration)
        {
            new SerilogInstaller().InstallServices(services, configuration);
            new RabbitMqInstaller().InstallServices(services, configuration);
            services.AddSingleton(configuration);
        }

        public static void InstallServicesFromNamespace(this IServiceCollection services, string assembly, string ns)
        {
            var interfaceTypes = TypeHelper.GetInterfaceTypesFromNamespace(assembly, ns);
            interfaceTypes.ForEach(interfaceType => {
                TypeHelper.GetAssignableTypesOfInterface(interfaceType).ForEach(classType => services.AddScoped(classType));
            });
        }

        public static void InstallServicesByInterface(this IServiceCollection services, Type interfaceType)
        {
            TypeHelper.GetAssignableTypesOfInterface(interfaceType).ForEach(classType => services.AddScoped(classType));
        }


    }
}
