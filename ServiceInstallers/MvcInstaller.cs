using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace Framework.ServiceInstallers
{
    public class MvcInstaller : IApiInstaller
    {

        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            services.AddMvc().AddNewtonsoftJson(action => action.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddHttpContextAccessor();
        }
    }
}
