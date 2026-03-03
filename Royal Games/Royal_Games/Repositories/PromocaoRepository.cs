using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class PromocaoRepository : IPromocaoRepository
    {
        private readonly RoyalGamesContext _context;

        public PromocaoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Promocao> Listar()
        {
            return _context.Promocao.ToList();
        }

        public Promocao ObterPorId(int id)
        {
           Promocao promocao = _context.Promocao.FirstOrDefault(p => p.PromocaoID == id);
            
            return promocao;
        }

        public bool NomeExiste(string nome, int? promocaIdAtual = null)
        {
            var consulta = _context.Promocao.AsQueryable();
            if (promocaIdAtual.HasValue)
            { 
                consulta = consulta.Where(p => p.PromocaoID != promocaIdAtual.Value);
            }

            return consulta.Any(p => p.Nome == nome);

        }

        public void Adicionar(Promocao Promocao)
        {
            _context.Promocao.Add(Promocao);
            _context.SaveChanges();
        }

        public void Atualizar(Promocao Promocao)
        {
            Promocao? promocaoBanco = _context.Promocao.FirstOrDefault(p => p.PromocaoID == Promocao.PromocaoID);

            if (promocaoBanco != null) 
            {
                return;
            }

            promocaoBanco.Nome = Promocao.Nome;
            promocaoBanco.DataExpiracao = Promocao.DataExpiracao;
            promocaoBanco.StatusPromocao = Promocao.StatusPromocao;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Promocao? promocaoBanco = _context.Promocao.FirstOrDefault(p => p.PromocaoID == id);
            if (promocaoBanco != null)
            {
                return;
            }
            _context.Promocao.Remove(promocaoBanco);
            _context.SaveChanges();
        }

    }
}
