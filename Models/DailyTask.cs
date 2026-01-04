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

        bool isTodayTask = dailyTask.Date > today.Subtract(new TimeSpan(23, 59, 59));
        bool taskNotCompleted = dailyTask.MinutesCompleted < dailyTask.TotalMinutes;
        bool taskCompleted = dailyTask.MinutesCompleted == dailyTask.TotalMinutes;
        bool taskOverDone = dailyTask.MinutesCompleted > dailyTask.TotalMinutes;

        //Tarea que no ha sido iniciada pero todavia se puede hacer
        if (isTodayTask && taskNotCompleted)
            return DailyTaskProgress.NotDone;

        if (taskCompleted)
            return DailyTaskProgress.Done;

        if (taskOverDone)
            return DailyTaskProgress.Overdone;

        return DailyTaskProgress.Incomplete;
    }
}