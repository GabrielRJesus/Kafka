using App.Repositorios;
using App.Repositorios.Interfaces;
using App.Services;
using App.Services.Interfaces;
using Confluent.Kafka;
using System.Text.Json;

IAppMensagemRepositorio repositorio = new KafkaMensagemRepositorio();
IAppMensagemService service = new AppMensagemService(repositorio);

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092", // Endereço e porta do servidor Kafka
};

CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    var topics = new string[] {"pessoa", "proposta", "hierarquia"};
    var listPessoa = service.GetPessoaList();
    var listProposta = service.GetPropostaList();
    var listHierarquia = service.GetHierarquiaList();

    while (!cts.IsCancellationRequested)
    {
        try
        {
            Random random = new Random();
            var indexTopic = random.Next(topics.Length);
            var indexPessoa = random.Next(listPessoa.Count);
            var indexProposta = random.Next(listProposta.Count);
            var indexHierarquia = random.Next(listHierarquia.Count);
            var topic = topics[indexTopic];
            var message = String.Empty;
            switch (topic)
            {
                case "pessoa":
                    message = JsonSerializer.Serialize(listPessoa[indexPessoa]);
                    break;
                case "proposta":
                    message = JsonSerializer.Serialize(listProposta[indexProposta]);
                    break;
                default:
                    message = JsonSerializer.Serialize(listHierarquia[indexHierarquia]);
                    break;
            }
            var deliveryReport = producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

            deliveryReport.ContinueWith(task =>
            {
                Console.WriteLine($"Mensagem enviada para tópico: {topic}, Partição: {task.Result.Partition}, Offset: {task.Result.Offset}");
            });

            producer.Flush(TimeSpan.FromSeconds(10));

            Thread.Sleep(TimeSpan.FromSeconds(30));
        }
        catch(OperationCanceledException oce)
        {
            continue;
        }
    }
}