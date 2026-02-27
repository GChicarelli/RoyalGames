using System;
using System.Collections.Generic;

namespace Royal_Games.Domains;

public partial class JogoPromocao
{
    public int PromocaoID { get; set; }

    public int JogoID { get; set; }

    public decimal PrecoAtual { get; set; }

    public virtual Jogo Jogo { get; set; } = null!;

    public virtual Promocao Promocao { get; set; } = null!;
}
