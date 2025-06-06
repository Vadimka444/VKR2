using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Reason
{
    public int Reasoncd { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Attendancesociety> Attendancesocieties { get; set; } = new List<Attendancesociety>();
}
