namespace Kafka.Consumer.Models.Models
{
    public class Proposta
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public DateTime Data { get; set; }
        public double Valor { get; set; }
        public int Parcelas { get; set; }

        public Proposta(int id, int pessoaId, DateTime data, double valor, int parcelas)
        {
            Id = id;
            PessoaId = pessoaId;
            Data = data;
            Valor = valor;
            Parcelas = parcelas;
        }

        public override string ToString()
        {
            return $"{Id} - {PessoaId} - {Data} - {Valor} - {Parcelas}";
        }
    }
}
