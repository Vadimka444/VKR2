using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Nachislsumma
{
    public int Nachislfactcd { get; set; }

    public int Pupilcd { get; set; }

    public int Societycd { get; set; }

    public decimal Nachislsum { get; set; }

    public short Nachislmonth { get; set; }

    public short Nachislyear { get; set; }

    public virtual Pupil Pupil { get; set; } = null!;

    public virtual Society Society { get; set; } = null!;
}
