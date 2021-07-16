using Background.Consumers;
using Framework.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Framework.ServiceInstallers
{
    public static class InstallerExtension
    {
        public static void InstallApiServices<TContext, TState, TStartup>(this IServiceCollection services, IConfiguration configuration, List<Type> interfaces = null) 
            where TContext : DbContext
            where TState : class, new()
        {
            TypeHelper.CreateObjects<IInstaller>().ForEach(rc => rc.InstallServices(services, configuration, typeof(TStartup)));
            TypeHelper.CreateObjects<IApiInstaller>().ForEach(rc => rc.InstallServices(services, configuration, typeof(TStartup)));
            new DataInstaller<TContext>().InstallServices(services, configuration);
            services.AddScoped<TState>();
            interfaces?.ForEach(intface => services.InstallServicesFromInterface(intface));
        }

        public static void InstallConsoleStartupServices<TContext, TState, TStartup>(this IServiceCollection services, IConfiguration configuration, List<Type> interfaces = null)
            where TContext : DbContext
            where TState : class, new()
        {
            TypeHelper.CreateObjects<IInstaller>().ForEach(rc => rc.InstallServices(services, configuration, typeof(TStartup)));
            TypeHelper.CreateObjects<IConsoleInstaller>().ForEach(rc => rc.InstallServices(services, configuration, typeof(TStartup)));
            new DataInstaller<TContext>().InstallServices(services, configuration);
            interfaces?.ForEach(intface => services.InstallServicesFromInterface(intface));
            services.AddSingleton(new TState());
            services.AddHostedService<ConsoleHostedService<TContext, TState, TStartup>>();
            services.AddHostedService<ConsoleSchedularService<TContext, TState, TStartup>>();
        }

        public static void InstallServicesFromInterface(this IServiceCollection services, Type typeOfInterface)
        {
            var types = TypeHelper.GetAssignableTypes(typeOfInterface);
            types.ForEach(type => services.AddScoped(type));
        }
    }
}
