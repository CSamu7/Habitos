using System;
using System.Collections.Generic;

namespace Habits.Models;

public partial class Task
{
    public int IdTask { get; set; }

    public string Name { get; set; } = null!;

    public int Minutes { get; set; }

    public int RepeatedEvery { get; set; }

    public byte[]? UnavailableDays { get; set; }

    public bool IsActive { get; set; }

    public int IdUser { get; set; }

    public int? IdGroup { get; set; }

    public virtual ICollection<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();

    public virtual Group? IdGroupNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<SchedulesTask> SchedulesTasks { get; set; } = new List<SchedulesTask>();
}
