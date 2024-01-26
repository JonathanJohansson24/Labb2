using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Personal
{
    public int PersonalId { get; set; }

    public string Personalförnamn { get; set; } = null!;

    public string Personalefternamn { get; set; } = null!;

    public int BefattningsId { get; set; }

    public virtual Befattningar Befattnings { get; set; } = null!;

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
