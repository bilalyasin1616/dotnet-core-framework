using Framework.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Framework.RequestConfigurators
{
    public static class RequestExtension
    {
        public static void ConfigureRequest(this IApplicationBuilder app, IConfiguration configuration)
        {
            TypeHelper.CreateObjects<IRequestConfigurator>().ForEach(rc => rc.ConfigureRequest(app, configuration));
        }
    }
}