using Framework.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public class ConsoleStartupInstaller<TStartup> where TStartup : class
    {
        private IConfiguration Configuration { get; }
        public IServiceProvider ServiceProvider { get; }
        public ConsoleStartupInstaller(Action<IServiceCollection> configureServices = default)
        {
            Configuration = BuildConfiguration();
            var services = new ServiceCollection();
            services.InstallConsoleStartupServices(Configuration);
            services.AddSingleton<TStartup>();
            configureServices?.Invoke(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false)
                .AddJsonFile($"appsettings.{EnvironmentHelper.GetEnvironment()}.json", true, true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

    }
}
