using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Workerspost
{
    public int Workerpostcd { get; set; }

    public int Workercd { get; set; }

    public int Postcd { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Worker Worker { get; set; } = null!;
}
