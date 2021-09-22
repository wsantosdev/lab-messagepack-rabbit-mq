using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.MessagePack.RabbitMq
{
    public class JsonBrokerConsumerService : BackgroundService
    {
        private readonly ConnectionFactory factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
        private IConnection _connection;
        private IModel _channel;
        private ILogger<BackgroundService> _logger;

        public JsonBrokerConsumerService(ILogger<JsonBrokerConsumerService> logger)
        {
            _logger = logger;
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("json", ExchangeType.Direct, true);
            _channel.QueueDeclare("json-queue", true, false, false);
            _channel.QueueBind("json-queue", "json", string.Empty, null);
            _channel.BasicQos(0, 1, false);
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            /*var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var content = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonSerializer.Deserialize<Aluno>(content);

                _logger.LogInformation("Json message received: {0}", message.Nome);

                _channel.BasicAck(args.DeliveryTag, false);
            };

            _channel.BasicConsume("json-queue", false, consumer);*/

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
