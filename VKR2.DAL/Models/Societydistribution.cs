using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Societydistribution
{
    public int Distributioncd { get; set; }

    public int Pupilcd { get; set; }

    public int Societycd { get; set; }

    public virtual Pupil Pupil { get; set; } = null!;

    public virtual Society Society { get; set; } = null!;
}
