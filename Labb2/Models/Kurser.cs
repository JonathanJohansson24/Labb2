using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Kurser
{
    public int KursId { get; set; }

    public string? Kursnamn { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
