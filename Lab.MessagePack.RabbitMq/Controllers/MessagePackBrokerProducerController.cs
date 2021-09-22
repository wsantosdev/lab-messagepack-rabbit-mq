using MessagePack;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Lab.MessagePack.RabbitMq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagePackBrokerProducerController : ControllerBase
    {
        private readonly ConnectionFactory factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };

        [HttpPost]
        public IActionResult ProduceMessage(AlunoComSala aluno)
        {
            PublishMessage(aluno);

            return Ok(aluno.Nome);
        }

        private void PublishMessage(AlunoComSala aluno)
        {
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("message-pack-queue", true, false, false);

            var binary = MessagePackSerializer.Serialize(aluno);
            channel.BasicPublish("message-pack", string.Empty, null, binary);
        }
    }
}
