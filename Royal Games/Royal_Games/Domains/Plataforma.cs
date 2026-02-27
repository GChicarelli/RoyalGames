using System;
using System.Collections.Generic;

namespace Royal_Games.Domains;

public partial class Plataforma
{
    public int PlataformaID { get; set; }

    public string? Nome { get; set; }

    public virtual ICollection<Jogo> Jogo { get; set; } = new List<Jogo>();
}
