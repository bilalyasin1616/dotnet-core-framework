using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace Framework.ServiceInstallers
{
    public class MongoInstalller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, Type typeOfStartup)
        {
            services.AddScoped<IMongoClient, MongoClient>(options => new MongoClient(configuration["Database:MongoConnection"]));
        }
    }
}
