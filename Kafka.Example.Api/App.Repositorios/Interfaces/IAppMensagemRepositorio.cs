using App.Modelos.Entidades;

namespace App.Repositorios.Interfaces
{
    public interface IAppMensagemRepositorio
    {
        void EnviaMensagem(AppMensagem mensagem);
    }
}
