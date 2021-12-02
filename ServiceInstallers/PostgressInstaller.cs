using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Framework.ServiceInstallers
{
    public class PostgressInstaller<C> : IPostgressInstaller where C : DbContext
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            services.AddDbContext<C>(options => options.UseNpgsql(configuration["Database:PostgressConnection"], server => server.MigrationsAssembly(typeOfStartup.Assembly.GetName().Name)));
        }
    }
}