using Confluent.Kafka;
using Framework.Exceptions;
using Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Kafka
{
    public class Consumer
    {
        //private ConsumerConfig ConsumerConfig { get; set; }

        //public Consumer(ConsumerConfig consumerConfig)
        //{
        //    ConsumerConfig = consumerConfig;
        //    ConsumerConfig.EnableAutoCommit = false;
        //    ConsumerConfig.AutoOffsetReset = AutoOffsetReset.Earliest;
        //}

        //public void ConsumeTopic<T, U>(string topicName) where T : DbContext where U : TopicBase<T>
        //{
        //    ThreadPool.QueueUserWorkItem(obj => ConsumeInBackground<T, U>(topicName));
        //}

        //public void ConsumeInBackground<T, U>(string topicName) where T : DbContext where U : TopicBase<T>
        //{
        //    using (var consumer = new ConsumerBuilder<Ignore, string>(ConsumerConfig).Build())
        //    {
        //        consumer.Subscribe(topicName);
        //        while (true)
        //        {
        //            try
        //            {
        //                var consumerResult = consumer.Consume();
        //                Console.WriteLine($"Message receive from partition {consumerResult.TopicPartitionOffset} with offset {consumerResult.Offset}");
        //                using (var topic = Factory.GetTopic<T, U>())
        //                {
        //                    var method = topic.GetType().GetMethod(topicName);
        //                    if (method == null)
        //                        throw new CustomException($"Topic {topicName} function is not defined in class {typeof(U).Name}");
        //                    method.Invoke(topic, new object[] { consumerResult.Message.Value });
        //                }
        //                consumer.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                Log.Logger.LogError(ex, $"Exception in consuming from topic");
        //            }
        //        }
        //    }
        //}


    }
}
