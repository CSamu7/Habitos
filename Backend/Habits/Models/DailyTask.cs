namespace Habits.Models;

public partial class DailyTask
{
    public int IdDailyTask { get; set; }

    public int IdRoutine { get; set; }

    public DateTimeOffset? CompletedAt { get; set; }

    public int MinutesCompleted { get; set; }

    public int TotalMinutes { get; set; }

    public DateTimeOffset Date { get; set; }

    public virtual Routine IdRoutineNavigation { get; set; } = null!;
}
