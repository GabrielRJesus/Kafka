namespace App.Modelos.Entidades
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
    }
}
