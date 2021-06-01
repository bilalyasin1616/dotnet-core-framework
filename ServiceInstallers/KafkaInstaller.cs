using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.ServiceInstallers
{
    public class KafkaInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig();
            configuration.Bind("KafkaConfig", producerConfig);
            services.AddSingleton(producerConfig);
        }
    }
}
