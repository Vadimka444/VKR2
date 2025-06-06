using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Parent
{
    public int Parentcd { get; set; }

    public string Fio { get; set; } = null!;

    public string Passport { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Familyconnection> Familyconnections { get; set; } = new List<Familyconnection>();
}
