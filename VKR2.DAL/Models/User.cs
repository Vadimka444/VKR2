using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class User
{
    public int Usercd { get; set; }

    public string Email { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public int? Workercd { get; set; }

    public int? Parentcd { get; set; }

    public virtual ICollection<Rolesuser> Rolesusers { get; set; } = new List<Rolesuser>();
}
