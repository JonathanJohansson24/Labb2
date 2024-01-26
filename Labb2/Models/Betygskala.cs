using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Betygskala
{
    public int BetygskalaId { get; set; }

    public string? Betydelse { get; set; }

    public int? Betygpoäng { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
