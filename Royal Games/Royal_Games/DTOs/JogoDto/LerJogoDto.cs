namespace Royal_Games.DTOs.JogoDto
{
    public class LerJogoDto
    {
        public string JogoID { get; set; }

        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public bool? StatusJogo { get; set; }

        // Categorias 

        public List<int> CategoriasIds { get; set; }

        public List<string> Categoria { get; set; }

        // Usuario

        public int? UsuarioID { get; set; }

        public string? UsuarioNome { get; set; }

        public string? UsuarioEmail { get; set; }
    }
}
