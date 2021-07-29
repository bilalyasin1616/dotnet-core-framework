using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public class CorsInstaller : IApiInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            if (configuration.GetSection("CorsePolicy") == null)
                throw new Exception("Couldn't configure service, define CorsePolicy configuration section in config files");
            if (configuration["CorsePolicy:Name"] == null)
                throw new Exception("Couldn't configure service, define CorsePolicy:Name configuration in config files");
            services.AddCors(o => o.AddPolicy(configuration["CorsePolicy:Name"], builder =>
            {
                var origins = configuration.GetSection("CorsePolicy:Origins");
                builder = origins.Value != null ? builder.WithOrigins(origins.Get<string[]>()) : builder.AllowAnyOrigin();

                var methods = configuration.GetSection("CorsePolicy:Methods");
                builder = methods.Value != null ? builder.WithMethods(methods.Get<string[]>()) : builder.AllowAnyMethod();

                var headers = configuration.GetSection("CorsePolicy:Headers");
                builder = headers.Value != null ? builder.WithHeaders(headers.Get<string[]>()) : builder.AllowAnyHeader();
            }));
        }
    }
}
