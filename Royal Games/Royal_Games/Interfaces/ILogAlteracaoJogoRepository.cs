using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface ILogAlteracaoJogoRepository
    {
        List<Log_AlteracaoJogo> Listar();
        List<Log_AlteracaoJogo> ListarPorJogo(int jogoId);

    }
}
