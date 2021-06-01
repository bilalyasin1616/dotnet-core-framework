using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Framework.ServiceInstallers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().AddNewtonsoftJson(action => action.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddHttpContextAccessor();
            services.AddSingleton(configuration);
        }
    }
}
