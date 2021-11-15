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
            if(configuration["Database:MongoConnection"]==null)
            {
                Console.WriteLine("MongoDb is not configured to be used, make sure you have 'Database:MongoConnection' property defined in your config file");
                return;
            }
            services.AddScoped<IMongoClient, MongoClient>(options => new MongoClient());
        }
    }
}
