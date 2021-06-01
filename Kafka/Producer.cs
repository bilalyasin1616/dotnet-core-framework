using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Kafka
{
    public static class Producer
    {
        public static void Produce<T>(this ProducerConfig producerConfig, T request, string topicName)
        {
            var producerBuilder = new ProducerBuilder<Null, string>(producerConfig);
            using (var producer = producerBuilder.Build())
            {
                var serializedRequest = JsonConvert.SerializeObject(request);
                producer.ProduceAsync(topicName, new Message<Null, string>() { Value = serializedRequest });
                producer.Flush();
            }
        }
    }
}
