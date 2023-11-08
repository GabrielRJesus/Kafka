using Confluent.Kafka;
using Kafka.Consumer.Models.Models;
using System.Text.Json;

class Program
{

    static void Main(string[] args)
    {

        IConsumer<Ignore, string> _consumer;

        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092", // Endereço e porta do servidor Kafka
            GroupId = "meu-grupo",
            AutoOffsetReset = AutoOffsetReset.Earliest, // Pode ser 'Earliest' ou 'Latest'
        };

        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        var topics = new string[] { "pessoa", "proposta", "hierarquia" };
        _consumer.Subscribe(topics);

        // Iniciar um loop em segundo plano para processar mensagens
        while (true)
        {
            try
            {
                var consumeResult = _consumer.Consume();
                var message = consumeResult.Message.Value;
                var resposta = String.Empty;
                switch (consumeResult.Topic)
                {
                    case "pessoa":
                        var pessoa = JsonSerializer.Deserialize<Pessoa>(message);
                        resposta = pessoa.ToString();
                        break;
                    case "proposta":
                        var proposta = JsonSerializer.Deserialize<Proposta>(message);
                        resposta = proposta.ToString();
                        break;
                    default:
                        var hierarquia = JsonSerializer.Deserialize<Hierarquia>(message);
                        resposta = hierarquia.ToString();
                        break;
                }
                Console.WriteLine($"Mensagem recebida do tópico {consumeResult.Topic}, Partição {consumeResult.Partition}, Offset {consumeResult.Offset}: {resposta}");
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Erro ao consumir a mensagem: {e.Error.Reason}");
            }
        }
    }
}