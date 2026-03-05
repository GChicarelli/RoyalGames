using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Royal_Games.DTOs.JogoDto
{
    public class AtualizarJogoDto
    {
        public string Nome { get; set; } = null;

        public decimal Preco { get; set; }

        public string Descricao { get; set; }

        public IFormFile Imagem { get; set; }

        public List<int> GeneroIds { get; set; }

        public bool? StatusJogo { get; set; }
    }
}
