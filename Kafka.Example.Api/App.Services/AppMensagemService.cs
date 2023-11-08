using App.Modelos.Entidades;
using App.Repositorios.Interfaces;
using App.Services.Interfaces;

namespace App.Services
{
    public class AppMensagemService : IAppMensagemService
    {
        private readonly IAppMensagemRepositorio _repositorio;

        public AppMensagemService(IAppMensagemRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public void EnviarMensagem(AppMensagem mensagem)
        {
            _repositorio.EnviaMensagem(mensagem);
        }

        public List<Pessoa> GetPessoaList()
        {
            return new List<Pessoa>()
            {
                new Pessoa(1, "Gabriel", 34),
                new Pessoa(2, "Jonathas", 30),
                new Pessoa(3, "Fabricio", 30),
                new Pessoa(4, "Heitor", 2)
            };
        }

        public List<Hierarquia> GetHierarquiaList()
        {
            return new List<Hierarquia>()
            {
                new Hierarquia(1,2,"Desenvolvedor"),
                new Hierarquia(3,2,"Desenvolvedor"),
                new Hierarquia(2,4,"Supervisor de desenvolvimento")
            };
        }

        public List<Proposta> GetPropostaList()
        {
            return new List<Proposta>()
            {
                new Proposta(1,1,new DateTime(2023,11,07), 2200, 3),
                new Proposta(2,2,new DateTime(2023,11,07), 5000, 5),
                new Proposta(3,3,new DateTime(2023,11,07), 22000, 7),
                new Proposta(4,2,new DateTime(2023,11,07), 9002, 10),
                new Proposta(5,3,new DateTime(2023,11,07), 7683, 6),
                new Proposta(6,4,new DateTime(2023,11,07), 100, 4),
            };
        }
    }
}
