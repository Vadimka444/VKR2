using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Worker
{
    public int Workercd { get; set; }

    public string Fio { get; set; } = null!;

    public string Passport { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public DateOnly Dateofbirth { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Schedulesociety> Schedulesocieties { get; set; } = new List<Schedulesociety>();

    public virtual ICollection<Workersingroup> Workersingroups { get; set; } = new List<Workersingroup>();

    public virtual ICollection<Workerspost> Workersposts { get; set; } = new List<Workerspost>();
}
