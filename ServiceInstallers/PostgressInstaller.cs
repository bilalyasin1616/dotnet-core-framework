using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.ServiceInstallers
{
    public class PostgressInstaller<C>: IPostgressInstaller where C : DbContext
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<C>(options => options.UseNpgsql(configuration["Database:PastgressConnection"]));
        }
    }
}
