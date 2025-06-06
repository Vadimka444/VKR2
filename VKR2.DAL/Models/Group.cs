using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Group
{
    public int Groupcd { get; set; }

    public string Title { get; set; } = null!;

    public int Minage { get; set; }

    public int Maxage { get; set; }

    public int Numberofseats { get; set; }

    public virtual ICollection<Groupdistribution> Groupdistributions { get; set; } = new List<Groupdistribution>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Workersingroup> Workersingroups { get; set; } = new List<Workersingroup>();
}
