using Royal_Games.Applications.Conversoes;
using Royal_Games.Applications.Regras;
using Royal_Games.Domains;
using Royal_Games.DTOs.JogoDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;



namespace Royal_Games.Applications.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            List<LerJogoDto> produtoDto = jogos.Select(JogoParaDto.ConverterParaDto).ToList();
            return produtoDto;
        }

        public LerJogoDto ObterPorID(int id)
        {
            Jogo jogo = _repository.ObterPorID(id);

            if (jogo == null)
            {
                throw new DomainException("Jogo não encontrado");
            }

            return JogoParaDto.ConverterParaDto(jogo);
        }

        private static void ValidarCadastro(CriarJogoDto jogoDto)
        {
            if (string.IsNullOrWhiteSpace(jogoDto.Nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior ou igual a 0");
            }

            if (string.IsNullOrWhiteSpace(jogoDto.Descricao))
            {
                throw new DomainException("Descrição é obrigatória.");
            }

            if (jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("Imagem é obrigatória.");
            }

            if (jogoDto.CategoriaIds == null || jogoDto.CategoriaIds.Count() == 0)
            {
                throw new DomainException("Jogo deve ter ao menos uma categoria.");
            }
        }

        public byte[] ObterImagem(int id)
        {
            var imagem = _repository.ObterImagem(id);

            if(imagem == null || imagem.Length == 0)
            {
                throw new DomainException("Imagem não encontrada.");
            }

            return imagem;
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioID)
        {
            ValidarCadastro(jogoDto);

            if(_repository.NomeExiste(jogoDto.Nome))
            {
                throw new DomainException("Jogo já existente.");
            }

            Jogo jogo = new Jogo()
            {
                Nome = jogoDto.Nome,
                Preco = jogoDto.Preco,
                Descricao = jogoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem),
                StatusJogo = true,
                UsuarioID = usuarioID
            };

            _repository.Adicionar(jogo, jogoDto.CategoriaIds);

            return JogoParaDto.ConverterParaDto(jogo);
        }

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogoBanco = _repository.ObterPorID(id);

            if(jogoBanco == null)
            {
                throw new DomainException("Jogo não foi encontrado.");
            }

            if (_repository.NomeExiste(jogoDto.Nome, jogoIdAtual: id))
            {
                throw new DomainException("Produto Deve ter ao menos uma categoria.");
            }

            if (jogoDto.CategoriaIds == null || jogoDto.CategoriaIds.Count() == 0)
            {
                throw new DomainException("Produto deve ter ao menos uma categoria.");
            }

            jogoBanco.Nome = jogoDto.Nome;
            jogoBanco.Preco = jogoDto.Preco;
            jogoBanco.Descricao = jogoDto.Descrição;

            if (jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }

            if (jogoDto.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogoDto.StatusJogo.Value;
            }

            _repository.Atualizar(jogoBanco, jogoDto.CategoriaIds);
            return JogoParaDto.ConverterParaDto(jogoBanco);
        }
        
        public void Remover (int id)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogo = _repository.ObterPorID(id);

            if (jogo == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
