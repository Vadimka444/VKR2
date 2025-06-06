using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Attendancesociety
{
    public int Visitingcd { get; set; }

    public int Pupilcd { get; set; }

    public int Societycd { get; set; }

    public DateOnly Visitdate { get; set; }

    public bool Availability { get; set; }

    public int? Reasoncd { get; set; }

    public virtual Pupil Pupil { get; set; } = null!;

    public virtual Reason? Reason { get; set; }

    public virtual Society Society { get; set; } = null!;
}
