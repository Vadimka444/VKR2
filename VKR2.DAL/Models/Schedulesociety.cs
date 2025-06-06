using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Schedulesociety
{
    public int Schedulecd { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public DateOnly Scheduledate { get; set; }

    public int Societycd { get; set; }

    public int? Cabinetcd { get; set; }

    public int? Workercd { get; set; }

    public virtual Cabinet? Cabinet { get; set; }

    public virtual Society Society { get; set; } = null!;

    public virtual Worker? Worker { get; set; }
}
