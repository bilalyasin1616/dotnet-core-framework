using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Framework.RequestConfigurators
{
    public class SwaggerConfigurator : IRequestConfigurator
    {
        public void ConfigureRequest(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{configuration["Swagger:Version"]}/swagger.json", configuration["Swagger:Title"]);
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
        }
    }
}
