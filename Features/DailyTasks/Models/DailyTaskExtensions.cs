using Habits.Features.DailyTasks.Endpoints;
using Habits.Features.Tasks.Models;
using Habits.Models;

namespace Habits.Features.DailyTasks.Models
{
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
        public static DailyTaskGetResponse ToDailyTaskGetResponse(this DailyTask dailyTask)
        {
            double percentage = (double) dailyTask.MinutesCompleted / dailyTask.TotalMinutes * 100;

            return new DailyTaskGetResponse(
                dailyTask.IdDailyTask,
                new SimpleTaskDTO(
                    dailyTask.IdTaskNavigation.IdTask,
                    dailyTask.IdTaskNavigation.Name
                ),
                dailyTask.MinutesCompleted,
                dailyTask.TotalMinutes,
                percentage,
                dailyTask.CompletedAt
            );
        }
    }
}
