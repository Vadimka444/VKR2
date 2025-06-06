using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Society
{
    public int Societycd { get; set; }

    public string Title { get; set; } = null!;

    public int Minage { get; set; }

    public int Maxage { get; set; }

    public decimal Price { get; set; }

    public int Numberofseats { get; set; }

    public virtual ICollection<Attendancesociety> Attendancesocieties { get; set; } = new List<Attendancesociety>();

    public virtual ICollection<Nachislsumma> Nachislsummas { get; set; } = new List<Nachislsumma>();

    public virtual ICollection<Paysumma> Paysummas { get; set; } = new List<Paysumma>();

    public virtual ICollection<Schedulesociety> Schedulesocieties { get; set; } = new List<Schedulesociety>();

    public virtual ICollection<Societydistribution> Societydistributions { get; set; } = new List<Societydistribution>();
}
