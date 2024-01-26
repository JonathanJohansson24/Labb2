using System;
using System.Collections.Generic;

namespace Labb2.Models;

public partial class Betyg
{
    public int BetygId { get; set; }

    public int? KursId { get; set; }

    public int? BetygskalaId { get; set; }

    public int? ElevId { get; set; }

    public int? PersonalId { get; set; }

    public DateOnly? Datumförbetyg { get; set; }

    public virtual Betygskala? Betygskala { get; set; }

    public virtual Studenter? Elev { get; set; }

    public virtual Kurser? Kurs { get; set; }

    public virtual Personal? Personal { get; set; }
}
