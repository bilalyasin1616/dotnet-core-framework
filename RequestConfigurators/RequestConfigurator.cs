using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Framework.RequestConfigurators
{
    public class RequestConfigurator : IRequestConfigurator
    {
        public void ConfigureRequest(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseCors(configuration["CorsePolicy:Name"]);
            app.UseMiddleware<ExceptionHandler>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
