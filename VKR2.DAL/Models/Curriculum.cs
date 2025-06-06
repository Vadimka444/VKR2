using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Curriculum
{
    public int Lessoncd { get; set; }

    public string Title { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public short Quantity { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
