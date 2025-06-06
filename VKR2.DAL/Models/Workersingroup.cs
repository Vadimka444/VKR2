using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Workersingroup
{
    public int Workeringroupcd { get; set; }

    public int Groupcd { get; set; }

    public int Workercd { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Worker Worker { get; set; } = null!;
}
