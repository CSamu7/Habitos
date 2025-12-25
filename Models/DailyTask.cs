using System;
using System.Collections.Generic;

namespace Habits.Models;

public partial class DailyTask
{
    public int IdDailyTask { get; set; }

    public int IdTask { get; set; }

    public DateTimeOffset? CompletedAt { get; set; }

    public int MinutesCompleted { get; set; }

    public int TotalMinutes { get; set; }

    public DateTimeOffset Date { get; set; }

    public virtual Task IdTaskNavigation { get; set; } = null!;
}
