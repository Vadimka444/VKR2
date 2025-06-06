using System;
using System.Collections.Generic;

namespace VKR2.DAL.Models;

public partial class Schedule
{
    public int Schedulecd { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public DateOnly Scheduledate { get; set; }

    public int Groupcd { get; set; }

    public int Lessoncd { get; set; }

    public int? Cabinetcd { get; set; }

    public int? Workercd { get; set; }

    public virtual Cabinet? Cabinet { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Curriculum Lesson { get; set; } = null!;

    public virtual Worker? Worker { get; set; }
}
