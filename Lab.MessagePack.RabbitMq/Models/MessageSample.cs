using MessagePack;

namespace Lab.MessagePack.RabbitMq
{
    [MessagePackObject]
    public struct MessageSample
    {
        [Key(0)]
        public string Body { get; set; }

        public MessageSample(string body)
            => Body = body;
    }
}
