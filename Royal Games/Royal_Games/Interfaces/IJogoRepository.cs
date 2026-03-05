using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();

        Jogo ObterPorID(int id);

        byte[] ObterImagem(int id);

        bool NomeExiste(string nome, int? jogoIdAtual = null);

        void Adicionar(Jogo jogo, List<int> categoriaIds);

        void Atualizar(Jogo jogo, List<int> categoriaIds);

        void Remover(int id);
    }
}