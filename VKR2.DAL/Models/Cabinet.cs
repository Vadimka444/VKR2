using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Cabinet
{
    public int Cabinetcd { get; set; }

    public int? Cabinetno { get; set; }

    public string Location { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Schedulesociety> Schedulesocieties { get; set; } = new List<Schedulesociety>();
}
