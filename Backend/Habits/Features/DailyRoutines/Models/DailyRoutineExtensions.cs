using Habits.Common.DailyRoutines;
using Habits.Models;

namespace Habits.Features.DailyTasks.Models
{
    public static class DailyRoutineExtensions
    {
        public static Progress GetProgress(this DailyRoutine dailyTask)
        {
            DateTimeOffset today = DateTimeOffset.UtcNow;

            bool isTaskActive = dailyTask.Date > today.Subtract(new TimeSpan(23, 59, 59));
            bool isTaskStarted = dailyTask.MinutesCompleted < dailyTask.TotalMinutes && dailyTask.TotalMinutes > 0;

            bool isTaskCompleted = dailyTask.MinutesCompleted == dailyTask.TotalMinutes;
            bool isTaskOverdone = dailyTask.MinutesCompleted > dailyTask.TotalMinutes;

            if (isTaskActive && dailyTask.MinutesCompleted == 0)
                return Progress.NotDone;

            if (isTaskActive && isTaskStarted)
                return Progress.InProgress;

            if (isTaskCompleted)
                return Progress.Done;

            if (isTaskOverdone)
                return Progress.Overdone;

            return Progress.Incomplete;
        }
    }
}
