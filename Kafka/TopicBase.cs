using AutoMapper;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Framework.Kafka
{
    public abstract class TopicBase<T>: IDisposable where T : DbContext
    {
        protected T DbContext { get; set; }
        protected ProducerConfig ProducerConfig { get; set; }
        protected IConfiguration Configuration { get; set; }
        public TopicBase(ProducerConfig producerConfig, T dbContext, IConfiguration configuration)
        {
            DbContext = dbContext;
            ProducerConfig = producerConfig;
            Configuration = configuration;
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
