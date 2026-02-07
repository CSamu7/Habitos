using Habits.API.Tasks.DTO;
using Habits.Models;

namespace Habits.API.DailyTasks.DTO
{
    public class GetDailyTaskResponse
    {
        public int IdDailyTask { get; init; }
        public GetMinimalTaskResponse SimpleTask { get; init; }
        public int MinutesCompleted { get; init; }
        public int TotalMinutes { get; init; }
        public double PercentageCompleted { get; init; }
        public DateTimeOffset? CompletedAt { get; init; }
        public static GetDailyTaskResponse FromDailyTask(DailyTask dailyTask)
        {
            double percentage = (double)dailyTask.MinutesCompleted / dailyTask.TotalMinutes * 100;
            Habits.Models.Task task = dailyTask.IdTaskNavigation;

            return new GetDailyTaskResponse
            {
                IdDailyTask = dailyTask.IdDailyTask,
                SimpleTask = new GetMinimalTaskResponse(dailyTask.IdTask, dailyTask.IdTaskNavigation.Name),
                MinutesCompleted = dailyTask.MinutesCompleted,
                TotalMinutes = dailyTask.TotalMinutes,
                PercentageCompleted = percentage,
                CompletedAt = dailyTask.CompletedAt
            };
        }
    }
}
