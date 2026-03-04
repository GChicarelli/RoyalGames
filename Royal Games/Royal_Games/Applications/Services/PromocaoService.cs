using Royal_Games.Applications.Regras;
using Royal_Games.Domains;
using Royal_Games.DTOs.PromocaoDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class PromocaoService
    {
        private readonly IPromocaoRepository _repository;

        public PromocaoService(IPromocaoRepository repository)
        {
            _repository = repository;
        }

        public List<LerPromocaoDto> Listar()
        {
            List<Promocao> Promocoes = _repository.Listar();

             List<LerPromocaoDto> promocoesDto = Promocoes.Select(p => new LerPromocaoDto
             { 
                PromocaoID = p.PromocaoID,
                Nome = p.Nome,
                DataExpiracao = p.DataExpiracao,
                StatusPromocao = p.StatusPromocao
             }).ToList();

            return promocoesDto;
        }

        public LerPromocaoDto ObterPorId(int id)
        {
            Promocao promocao = _repository.ObterPorId(id);
            if (promocao == null)
            {
                throw new DomainException("Promoção não encontrada.");
            }
            LerPromocaoDto promocaoDto = new LerPromocaoDto
            {
                PromocaoID = promocao.PromocaoID,
                Nome = promocao.Nome,
                DataExpiracao = promocao.DataExpiracao,
                StatusPromocao = promocao.StatusPromocao
            };
            return promocaoDto;
        }

        private static void ValidarNome(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("O nome da promoção é obrigatório.");
            }
        }

        public void Adicionar(CriarPromocaoDto promoDto)
        {
            ValidarNome(promoDto.Nome);
            ValidarDataExpiracaoPromocao.ValidarDataExpiracao(promoDto.DataExpiracao);

            if(_repository.NomeExiste(promoDto.Nome))
                {
                throw new DomainException("Promoção já existente.");
            }

            Promocao promocao = new Promocao
            {
                Nome = promoDto.Nome,
                DataExpiracao = promoDto.DataExpiracao,
                StatusPromocao = promoDto.StatusPromocao
            };

            _repository.Adicionar(promocao);

        }

        public void Atualizar(int id, CriarPromocaoDto promoDto)
        {
            ValidarNome(promoDto.Nome);

            Promocao promocaoBanco = _repository.ObterPorId(id);

            if(promocaoBanco == null)
            {
                throw new DomainException("Promoção não encontrada.");
            }

            if(_repository.NomeExiste(promoDto.Nome,promocaIdAtual:id))
            {
                throw new DomainException("já existe outra promoção com esse nome");
            }

            promocaoBanco.Nome = promoDto.Nome;
            promocaoBanco.DataExpiracao = promoDto.DataExpiracao;
            promocaoBanco.StatusPromocao = promoDto.StatusPromocao;

            _repository.Atualizar(promocaoBanco);

        }

        public void Remover(int id)
        {
            Promocao promocaoBanco = _repository.ObterPorId(id);

            if(promocaoBanco == null)
            {
                throw new DomainException("Promoção não encontrada.");
            }

            _repository.Remover(id);
        }


    }
}
