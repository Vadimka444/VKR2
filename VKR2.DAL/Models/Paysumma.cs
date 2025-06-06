using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Paysumma
{
    public int Payfactcd { get; set; }

    public int Pupilcd { get; set; }

    public int Societycd { get; set; }

    public decimal Paysum { get; set; }

    public DateOnly Paydate { get; set; }

    public short Paymonth { get; set; }

    public short Payyear { get; set; }

    public virtual Pupil Pupil { get; set; } = null!;

    public virtual Society Society { get; set; } = null!;
}
