using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IPromocaoRepository
    {
        List<Promocao> Listar();
        Promocao ObterPorId(int id);
        bool NomeExiste(string nome, int? promocaIdAtual = null);
        void Adicionar(Promocao Promocao);
        void Atualizar(Promocao Promocao);
        void Remover(int id);
    }
}
