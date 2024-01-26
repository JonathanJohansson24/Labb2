using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Befattningar
{
    public int BefattningsId { get; set; }

    public string Befattningstyp { get; set; } = null!;

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
