using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Framework.ServiceInstallers
{
    class CorsInstaller : IApiInstaller
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
                builder = origins != null ? builder.WithOrigins(origins.Get<string[]>()) : builder.AllowAnyOrigin();

                var methods = configuration.GetSection("CorsePolicy:Methods");
                builder = methods != null ? builder.WithMethods(origins.Get<string[]>()) : builder.AllowAnyMethod();

                var headers = configuration.GetSection("CorsePolicy:Headers");
                builder = headers != null ? builder.WithHeaders(origins.Get<string[]>()) : builder.AllowAnyHeader();
            }));
        }
    }
}
