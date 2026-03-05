using Microsoft.EntityFrameworkCore;
using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public JogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            List<Jogo> Jogos = _context.Jogo


                .Include(jogo => jogo.Genero)

                .Include(jogo => jogo.Usuario)
                .ToList();

            return Jogos;
        }

        public Jogo ObterPorID(int id)
        {
            Jogo? Jogo = _context.Jogo
                .Include(jogoDb => jogoDb.Genero)
                .Include(jogoDb => jogoDb.UsuarioID)

            .FirstOrDefault(jogoDb => jogoDb.JogoID == id);

            return Jogo;
        }

        public bool NomeExiste(string nome, int? JogoIdAtual = null)
        {

            var JogoConsultado = _context.Jogo.AsQueryable();


            if (JogoIdAtual.HasValue)
            {
                JogoConsultado = JogoConsultado.Where(Jogo => Jogo.JogoID != JogoIdAtual.Value);
            }

            return JogoConsultado.Any(Jogo => Jogo.Nome == nome);
        }

        public byte[] ObterImagem(int id)
        {
            var Jogo = _context.Jogo
                .Where(Jogo => Jogo.JogoID == id)
                .Select(Jogo => Jogo.Imagem)
                .FirstOrDefault();

            return Jogo;
        }

        public void Adicionar(Jogo Jogo, List<int> GeneroIds)
        {
            List<Genero> Generos = _context.Genero

                .Where(Genero => GeneroIds.Contains(Genero.GeneroID))
                .ToList();


            Jogo.Genero = Generos;

            _context.Jogo.Add(Jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo Jogo, List<int> GeneroIds)
        {
            Jogo? JogoBanco = _context.Jogo
                .Include(Jogo => Jogo.Genero)
                .FirstOrDefault(JogoAux => JogoAux.JogoID == Jogo.JogoID);
            if (JogoBanco == null)
            {
                return;
            }
            JogoBanco.Nome = Jogo.Nome;
            JogoBanco.Preco = Jogo.Preco;
            JogoBanco.Descricao = Jogo.Descricao;

            if (Jogo.Imagem != null && Jogo.Imagem.Length > 0)
            {
                JogoBanco.Imagem = Jogo.Imagem;
            }

            if (Jogo.StatusJogo.HasValue)
            {
                JogoBanco.StatusJogo = Jogo.StatusJogo;
            }


            var Generos = _context.Genero
                .Where(Genero => GeneroIds.Contains(Genero.GeneroID))
                .ToList();

            JogoBanco.Genero.Clear();

            foreach (var Genero in Generos)
            {
                JogoBanco.Genero.Add(Genero);
            }

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Jogo Jogo = _context.Jogo.FirstOrDefault(Jogo => Jogo.JogoID == id);

            if (Jogo == null)
            {
                return;
            }

            _context.Jogo.Remove(Jogo);
            _context.SaveChanges();
        }
    }
}
