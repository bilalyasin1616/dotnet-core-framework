using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public class ConsoleServiceInstaller<TContext> where TContext : DbContext
    {
        private IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; }

        public ConsoleServiceInstaller(IConfiguration configuration, Type mapperProfileType, Action<IServiceCollection> configureServices)
        {
            Configuration = configuration;
            var services = new ServiceCollection();
            services.InstallConsoleRequestServices<TContext>(Configuration, mapperProfileType);
            configureServices.Invoke(services);
            ServiceProvider = services.BuildServiceProvider();
        }

    }
}
