using System;
using System.Collections.Generic;

namespace Habits.Models;

public partial class SchedulesTask
{
    public int IdSchedule { get; set; }

    public int IdTask { get; set; }

    public int Position { get; set; }

    public virtual Schedule IdScheduleNavigation { get; set; } = null!;

    public virtual Task IdTaskNavigation { get; set; } = null!;
}
