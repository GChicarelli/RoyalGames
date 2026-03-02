namespace Royal_Games.Interfaces
{
    public interface ILogAlteracaoJogoRepository
    {
        List<Log_AlteracaoProduto> Listar();
        List<Log_AlteracaoProduto> ListarPorProduto(int produtoId);

    }
}
