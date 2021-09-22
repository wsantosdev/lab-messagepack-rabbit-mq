using MessagePack;

namespace Lab.MessagePack.RabbitMq
{
    [MessagePackObject]
    public record Aluno
    {
        [Key(0)]
        public string Nome { get; set; }
        [Key(1)]
        public int Idade { get; set; }
        [Key(2)]
        public int Serie { get; set; }
    }

    public record AlunoComSala : Aluno
    {
        [Key(3)]
        public string Sala { get; set; }
    }
}
