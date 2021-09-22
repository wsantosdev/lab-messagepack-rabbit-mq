using MessagePack;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Lab.MessagePack.RabbitMq
{
    public class MessagePackBrokerConsumerService : BackgroundService
    {
        private readonly ConnectionFactory factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
        private IConnection _connection;
        private IModel _channel;
        private ILogger<BackgroundService> _logger;

        public MessagePackBrokerConsumerService(ILogger<MessagePackBrokerConsumerService> logger)
        {
            _logger = logger;
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("message-pack", ExchangeType.Direct, true);
            _channel.QueueDeclare("message-pack-queue", true, false, false);
            _channel.QueueBind("message-pack-queue", "message-pack", string.Empty, null);
            _channel.BasicQos(0, 1, false);
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            /*var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var message = MessagePackSerializer.Deserialize<Aluno>(args.Body);
                _logger.LogInformation("Message received: {0}", message.Nome);

                _channel.BasicAck(args.DeliveryTag, false);
            };

            _channel.BasicConsume("message-pack-queue", false, consumer);*/

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
