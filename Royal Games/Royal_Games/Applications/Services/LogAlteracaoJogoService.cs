using Microsoft.Identity.Client;
using Royal_Games.Domains;
using Royal_Games.DTOs.LogJogoDto;
using Royal_Games.Interfaces;


namespace Royal_Games.Applications.Services
{
    public class LogAlteracaoJogoService
    {
        private readonly ILogAlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService(ILogAlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<Log_AlteracaoJogo> logs = _repository.Listar();

            List<LerLogJogoDto> listaLogJogo = logs.Select(l => new LerLogJogoDto
            {
                LogID = l.Log_AlteracaoJogoID,
                JogoID = l.JogoID,
                NomeAnterior = l.NomeAnterior, 
                PrecoAnterior = l.PrecoAnterior,
                DataAlteracao = l.DataAlteracao
            }).ToList();

            return listaLogJogo;
        }

        public List<LerLogJogoDto> ListarPorJogo(int jogoId)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(jogoId); 

            List<LerLogJogoDto> listaLogProduto = logs.Select(log => new LerLogJogoDto
            {
                LogID = log.Log_AlteracaoJogoID,
                JogoID = log.JogoID,
                NomeAnterior = log.NomeAnterior,
                PrecoAnterior = log.PrecoAnterior,
                DataAlteracao = log.DataAlteracao
            }).ToList();

            return listaLogProduto; 
        }
    }
}
