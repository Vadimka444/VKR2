using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Rolesuser
{
    public int Rolesuserscd { get; set; }

    public int Rolecd { get; set; }

    public int Usercd { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
