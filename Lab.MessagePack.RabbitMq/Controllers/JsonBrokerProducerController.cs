using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text.Json;

namespace Lab.MessagePack.RabbitMq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonBrokerProducerController : ControllerBase
    {
        private readonly ConnectionFactory factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };

        [HttpPost]
        public IActionResult ProduceMessage(Aluno aluno)
        {
            PublishMessage(aluno);

            return Ok(aluno.Nome);
        }

        private void PublishMessage(Aluno aluno)
        {
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("json-queue", true, false, false);

            var binary = JsonSerializer.SerializeToUtf8Bytes(aluno);
            channel.BasicPublish("json", string.Empty, null, binary);
        }
    }
}
