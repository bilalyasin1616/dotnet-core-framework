using Framework.Extensions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Reflection;
using System.Text;

namespace Framework.Services
{
    public class RabbitMqService
    {

        ConnectionFactory _connectionFactory { get; }
        IConnection _connection { get; }
        IModel _channel { get; }

        public RabbitMqService(string hostname, int port, string username, string password)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = hostname,
                Port = port,
                UserName = username,
                Password = password,
            };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendRequest<T>(string queue, T obj)
        {
            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var message = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(message);
            var properties = _channel.CreateBasicProperties();
            properties.AppId = Assembly.GetEntryAssembly().GetName().Name.ToLower().Replace(".", "-");
            properties.MessageId = $"{properties.AppId}-{DateTime.UtcNow.Ticks}";
            properties.Persistent = true;
            _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body);
            Log.Logger.LogInformation("Request", message, $"Request send with requestId:{properties.MessageId} to queue:{queue}");
        }

        public void ReceiveRequest<T>(string queue, Action<T> syncFunction)
        {
            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var Consumer = new EventingBasicConsumer(_channel);
            Consumer.Received += (model, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                Console.WriteLine($"Received request with request id {args.BasicProperties.MessageId} from {queue}");
                var request = JsonConvert.DeserializeObject<T>(message);
                syncFunction(request);
                _channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                Log.Logger.LogInformation($"Request completed with request id {args.BasicProperties.MessageId} from {queue}");
            };
            _channel.BasicConsume(queue: queue, autoAck: false, consumer: Consumer);
        }

        public void SendRequest(string queue, string message)
        {
            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
            var properties = _channel.CreateBasicProperties();
            properties.AppId = Assembly.GetEntryAssembly().GetName().Name.ToLower().Replace(".", "-");
            properties.MessageId = $"{properties.AppId}-{DateTime.UtcNow.Ticks}";
            properties.Persistent = true;
            _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body);
            Log.Logger.LogInformation("Request", message, $"Request send with requestId:{properties.MessageId} to queue:{queue}");
        }

        public void ReceiveRequest(string queue, Action<string> syncFunction)
        {
            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                Log.Logger.LogInformation($"Received request with request id {args.BasicProperties.MessageId} from {queue}");
                syncFunction(message);
                _channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                Log.Logger.LogInformation($"Request completed with request id {args.BasicProperties.MessageId} from {queue}");
            };
            _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        }


        public void ReceiveRequest(string queue,Type objectType, Action<object> syncFunction)
        {
            _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, args) =>
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                Log.Logger.LogInformation($"Received request with request id {args.BasicProperties.MessageId} from {queue}"); 
                syncFunction(JsonConvert.DeserializeObject(message, objectType));
                _channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                Log.Logger.LogInformation($"Request completed with request id {args.BasicProperties.MessageId} from {queue}");
            };
            _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        }
    }
}
