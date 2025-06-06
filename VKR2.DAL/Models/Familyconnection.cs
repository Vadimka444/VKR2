using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Familyconnection
{
    public int Familyconnectioncd { get; set; }

    public int Pupilcd { get; set; }

    public int Parentcd { get; set; }

    public string Kinshipdegree { get; set; } = null!;

    public virtual Parent Parent { get; set; } = null!;

    public virtual Pupil Pupil { get; set; } = null!;
}
