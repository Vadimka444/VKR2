using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Post
{
    public int Postcd { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Workerspost> Workersposts { get; set; } = new List<Workerspost>();
}
