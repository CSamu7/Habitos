using Habits.API.Tasks.DTO;
using Habits.Models;

namespace Habits.API.DailyTasks.DTO
{
    public record DailyTaskGetResponse(
        int IdDailyTask,
        BasicTask Task,
        int MinutesCompleted,
        int TotalMinutes,
        double PercentageCompleted,
        DateTimeOffset? CompletedAt
    );
    public static class DailyTaskExtensions
    {
        public static DailyTaskGetResponse ToGetResponse(this DailyTask dailyTask)
        {
            double percentage = (double)dailyTask.MinutesCompleted / dailyTask.TotalMinutes * 100;
            Habits.Models.Task task = dailyTask.IdTaskNavigation;

            return new DailyTaskGetResponse(
                dailyTask.IdDailyTask,
                new BasicTask(
                    task.IdTask,
                    task.Name
                ),
                dailyTask.MinutesCompleted,
                dailyTask.TotalMinutes,
                percentage,
                dailyTask.CompletedAt
            );
        }
    }
}
