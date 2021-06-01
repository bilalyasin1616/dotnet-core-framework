using Confluent.Kafka;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Framework.Models
{
    /// <summary>
    /// Initilizes a service with the required dependencies
    /// </summary>
    /// <typeparam name="C">Type of DbContext</typeparam>
    /// <typeparam name="S">Type of State</typeparam>
    public class ServiceInitModel<C, S>
    {
        public S User { get; set; }
        public C Context { get; set; }
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment CurrentEnvironment { get; set; }
        public ProducerConfig ProducerConfig { get; set; }
    }
}
