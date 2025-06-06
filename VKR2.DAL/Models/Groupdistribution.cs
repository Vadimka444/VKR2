using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Groupdistribution
{
    public int Distributioncd { get; set; }

    public int Pupilcd { get; set; }

    public int Groupcd { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Pupil Pupil { get; set; } = null!;
}
