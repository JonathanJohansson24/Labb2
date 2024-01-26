using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Studenter
{
    public int ElevId { get; set; }

    public string Elevförnamn { get; set; } = null!;

    public string Elevefternamn { get; set; } = null!;

    public string Personnummer { get; set; } = null!;

    public string? Klassnamn { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
