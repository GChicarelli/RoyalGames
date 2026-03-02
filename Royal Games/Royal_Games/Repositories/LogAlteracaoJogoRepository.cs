using Royal_Games.Interfaces;
using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Applications.Services;

namespace Royal_Games.Repositories
{  
  
    public class LogAlteracaoJogoRepository : ILogAlteracaoJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public LogAlteracaoJogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Log_AlteracaoJogo> Listar()
        {
            List<Log_AlteracaoJogo> log = _context.Log_AlteracaoJogo.OrderByDescending(l => l DataAlteracao).ToList();
            return log;
        }

        public List<Log_AltercaoJogo> ListarPorProduto (int jogoId)
        {
            List<Log_AlteracaoJogo>AltercaoProduto = _context.Log_AlteracaoJogo
                .Where (log => log.FK_ProdutoID == jogoId)
                .OrderByDescending(l => l.DataAltercao)
                .ToList();
            return AlteracaoJogo; 

        }
    }
      
}
