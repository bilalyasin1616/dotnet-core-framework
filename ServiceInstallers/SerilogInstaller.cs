using Framework.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Framework.ServiceInstallers
{
    public class SerilogInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearchConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"{Assembly.GetEntryAssembly().GetName().Name.ToLower().Replace(".", "-")}-{EnvironmentHelper.GetEnvironment()}-{DateTime.UtcNow:yyyy-MM}"
                })
                .Enrich.WithProperty("Environment", EnvironmentHelper.GetEnvironment())
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
