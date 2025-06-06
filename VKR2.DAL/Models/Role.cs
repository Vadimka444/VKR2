using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Role
{
    public int Rolecd { get; set; }

    public string Rolename { get; set; } = null!;

    public virtual ICollection<Rolesuser> Rolesusers { get; set; } = new List<Rolesuser>();
}
