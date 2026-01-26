using Habits.Common.DailyTasks;
using Habits.Models;

namespace Habits.Features.DailyTasks.Models
{
    public static class DailyTaskExtensions
    {
        public static Progress GetProgress(this DailyTask dailyTask)
        {
            DateTimeOffset today = DateTimeOffset.UtcNow;

            bool isTodayTask = dailyTask.Date > today.Subtract(new TimeSpan(23, 59, 59));
            bool taskNotCompleted = dailyTask.MinutesCompleted < dailyTask.TotalMinutes;
            bool taskCompleted = dailyTask.MinutesCompleted == dailyTask.TotalMinutes;
            bool taskOverDone = dailyTask.MinutesCompleted > dailyTask.TotalMinutes;

            //Tarea que no ha sido iniciada pero todavia se puede hacer
            if (isTodayTask && taskNotCompleted)
                return Progress.NotDone;

            if (taskCompleted)
                return Progress.Done;

            if (taskOverDone)
                return Progress.Overdone;

            return Progress.Incomplete;
        }
    }
}
