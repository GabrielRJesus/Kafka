using App.Modelos.Entidades;

namespace App.Services.Interfaces
{
    public interface IAppMensagemService
    {
        void EnviarMensagem(AppMensagem mensagem);
        List<Pessoa> GetPessoaList();
        List<Hierarquia> GetHierarquiaList();
        List<Proposta> GetPropostaList();
    }
}
