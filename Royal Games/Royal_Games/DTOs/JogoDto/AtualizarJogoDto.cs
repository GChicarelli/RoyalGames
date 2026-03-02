using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Royal_Games.DTOs.JogoDto
{
    public class AtualizarJogoDto
    {
        public string Nome { get; set; } = null;

        public decimal Preco { get; set; }

        public string Descrição { get; set; }

        public IFormFile Imagem { get; set; }

        public List<int> CategoriasIds { get; set; }

        public bool? StatusJogo { get; set; }
    }
}
