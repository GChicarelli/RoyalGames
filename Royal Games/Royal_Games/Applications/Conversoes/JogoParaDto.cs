using Royal_Games.Domains;
using Royal_Games.DTOs.JogoDto;


namespace Royal_Games.Domains
{
    public class JogoParaDto
    {
        public static LerJogoDto ConverterParaDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                //Jogo

                JogoID = jogo.jogoID,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                StatusJogo = jogo.StatusJogo,

                // Categoria

                CategoriaIds = jogo.Categoria.Select(categoria => categoria.CategoriaID).ToList(),

                Categorias = jogo.Categoria.Select(categoria => categoria.Nome).ToList(),

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario.Nome,
                UsuarioEmail = jogo.Usuario.Email
            };
        }
    }
}
