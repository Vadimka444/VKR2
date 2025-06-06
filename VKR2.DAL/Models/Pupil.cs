using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Pupil
{
    public int Pupilcd { get; set; }

    public string Fio { get; set; } = null!;

    public string Birthcertificatenumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Dateofbirth { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Attendancesociety> Attendancesocieties { get; set; } = new List<Attendancesociety>();

    public virtual ICollection<Familyconnection> Familyconnections { get; set; } = new List<Familyconnection>();

    public virtual ICollection<Groupdistribution> Groupdistributions { get; set; } = new List<Groupdistribution>();

    public virtual ICollection<Nachislsumma> Nachislsummas { get; set; } = new List<Nachislsumma>();

    public virtual ICollection<Paysumma> Paysummas { get; set; } = new List<Paysumma>();

    public virtual ICollection<Societydistribution> Societydistributions { get; set; } = new List<Societydistribution>();
}
