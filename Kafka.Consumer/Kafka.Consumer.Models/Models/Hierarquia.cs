namespace Kafka.Consumer.Models.Models
{
    public class Hierarquia
    {
        public int IdPessoa { get; set; }
        public int IdChefe { get; set; }
        public string Cargo { get; set; }

        public Hierarquia(int idPessoa, int idChefe, string cargo)
        {
            IdPessoa = idPessoa;
            IdChefe = idChefe;
            Cargo = cargo;
        }

        public override string ToString()
        {
            return $"{IdPessoa} - {IdChefe} - {Cargo}";
        }
    }
}
