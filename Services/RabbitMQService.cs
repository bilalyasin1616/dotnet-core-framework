using Framework.Extensions;
using Framework.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ILogger logger;

        ConnectionFactory _connectionFactory { get; }
        IConnection _connection { get; set; }
        IModel _channel { get; set; }

        public RabbitMqService(ConnectionFactory connectionFactory, ILogger<RabbitMqService> logger)
        {
            _connectionFactory = connectionFactory;
            this.logger = logger;
        }

        private void CreateConnection()
        {
            if (_connection == null)
                _connection = _connectionFactory.CreateConnection();
            if (_channel == null)
                _channel = _connection.CreateModel();
        }

        public void SendRequest<TState>(string queue, object data, TState state)
        {
            CreateConnection();
            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var message = JsonConvert.SerializeObject(new ConsoleRequest<TState>()
            {
                Data = JsonConvert.SerializeObject(data),
                State = state
            });
            var body = Encoding.UTF8.GetBytes(message);
            var properties = _channel.CreateBasicProperties();
            properties.AppId = Assembly.GetEntryAssembly().GetName().Name.ToLower().Replace(".", "-");
            properties.MessageId = $"{properties.AppId}-{DateTime.UtcNow.Ticks}";
            properties.Persistent = true;
            _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body);
            logger.LogInformation($"Request send with requestId:{properties.MessageId} to queue:{queue}", message);
        }

        public void ReceiveRequest<TState>(string queue, Type objectType, Func<object, TState, ConsoleRequestMeta, Task<bool>> syncFunction)
        {
            CreateConnection();
            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                logger.LogInformation($"Received request with request id {args.BasicProperties.MessageId} from {queue}");
                var deserializedRequest = JsonConvert.DeserializeObject<ConsoleRequest<TState>>(message);
                var success = await syncFunction(JsonConvert.DeserializeObject(deserializedRequest.Data, objectType), deserializedRequest.State, deserializedRequest.Meta);
                if (success)
                {
                    logger.LogInformation($"Request completed with request id {args.BasicProperties.MessageId} from {queue}");
                }
                else
                {
                    logger.LogError($"Request failed to complete with request id {args.BasicProperties.MessageId} from {queue}");
                    SendFailedRequest($"{queue}-failed", message);
                }
                _channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
            };
            _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        }

        private void SendFailedRequest(string queue, string message)
        {
            CreateConnection();
            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
            var properties = _channel.CreateBasicProperties();
            properties.AppId = Assembly.GetEntryAssembly().GetName().Name.ToLower().Replace(".", "-");
            properties.MessageId = $"{properties.AppId}-{DateTime.UtcNow.Ticks}";
            properties.Persistent = true;
            _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body);
            logger.LogInformation($"Failed Request send with requestId:{properties.MessageId} to queue:{queue}", message);
        }
    }
}
