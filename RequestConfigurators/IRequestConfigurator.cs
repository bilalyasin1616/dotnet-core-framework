using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Framework.RequestConfigurators
{
    public interface IRequestConfigurator
    {
        public void ConfigureRequest(IApplicationBuilder app, IConfiguration configuration);
    }
}
