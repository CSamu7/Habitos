using Habits.Features.Tasks;

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

public static class DailyTaskExtensions
{
    public static DailyTaskProgress GetProgress(this DailyTask dailyTask)
    {
        DateTimeOffset today = DateTimeOffset.UtcNow;

        if (dailyTask.Date == today && dailyTask.MinutesCompleted == 0)
            return DailyTaskProgress.NotDone;

        double percentage = (dailyTask.MinutesCompleted * 100) / dailyTask.TotalMinutes;

        if (dailyTask.Date == today && percentage > 0 && percentage < 100)
            return DailyTaskProgress.NotDone;

        if (dailyTask.Date == today && percentage == 100)
            return DailyTaskProgress.Done;

        if (percentage > 100)
            return DailyTaskProgress.Overdone;

        return DailyTaskProgress.Incomplete;
    }
}