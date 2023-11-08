using Confluent.Kafka;
using Kafka.Consumer.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Kafka.Consumer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaController : ControllerBase
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConsumer<Ignore, string> _consumer;

        public KafkaController()
        {
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092", // Endereço e porta do servidor Kafka
                GroupId = "queue_kafka",
                AutoOffsetReset = AutoOffsetReset.Earliest, // Pode ser 'Earliest' ou 'Latest'
            };

            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
            var topics = new string[] { "pessoa", "proposta", "hierarquia" };
            _consumer.Subscribe(topics);

            // Iniciar um loop em segundo plano para processar mensagens
            Task.Factory.StartNew(ConsumeMessages);
        }

        private void ConsumeMessages()
        {
            while (true)
            {
                try
                {
                    var consumeResult = _consumer.Consume();
                    var message = consumeResult.Message.Value;
                    var objeto = JsonSerializer.Deserialize<Object>(message);
                    var resposta = String.Empty;
                    switch (objeto)
                    {
                        case Pessoa _:
                            var pessoa = (Pessoa)objeto;
                            resposta = pessoa.ToString();
                            break;
                        case Proposta _:
                            var proposta = (Proposta)objeto;
                            resposta = proposta.ToString();
                            break;
                        default:
                            var hierarquia = (Hierarquia)objeto;
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
}