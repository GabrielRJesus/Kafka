using App.Modelos.Entidades;
using App.Repositorios.Interfaces;
using Confluent.Kafka;
using System.Text.Json;

namespace App.Repositorios
{
    public class KafkaMensagemRepositorio : IAppMensagemRepositorio
    {
        public void EnviaMensagem(AppMensagem mensagem)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                string json = JsonSerializer.Serialize(mensagem);
                producer.Produce("queue_kafka",
                                        new Message<string, string>
                                        {
                                            Key = Guid.NewGuid().ToString(),
                                            Value = json
                                        });
            }
        }
    }
}
