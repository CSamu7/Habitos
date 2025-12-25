using Habits.Models;
using System.ComponentModel.DataAnnotations;

namespace Habits.Features.Tasks
{
    public record ResponseBase<T>(
        List<T> Results,
        int Total
    );
    public record DailyTasksStats<T>
        (List<T> Results, 
        int TotalMinutes,
        int MinutesCompleted,
        int MinutesLeft, string PercentageCompleted) 
        : ResponseBase<T>(Results, Results.Count());
    public record SimpleTaskDTO(
        int IdTask,
        string Name
    );
    public record GetDailyTaskDTO(
        int IdDailyTask,
        SimpleTaskDTO Task,
        int MinutesCompleted,
        int TotalMinutes,
        double percentageCompleted,
        DateTimeOffset? CompletedAt
    );
    public record GetTodayDailyTaskDTO(
        int IdDailyTask,
        SimpleTaskDTO Task,
        int MinutesCompleted, 
        int TotalMinutes,
        string percentageCompleted
    );

    public record PatchDailyTask(
        [Range(1, 480, ErrorMessage = "Los minutos deben ser mayores a 0 y menores a 480")]
        int Minutes,
        DateTimeOffset Time,
        PatchOperations Operation
    );
    public static class DailyTaskDTOExtension
    {
        public static GetTodayDailyTaskDTO MapTodayDTO(this DailyTask dailyTask)
        {
            double percentage = ((double)dailyTask.MinutesCompleted / dailyTask.TotalMinutes) * 100;

            return new GetTodayDailyTaskDTO(
                dailyTask.IdDailyTask,
                new SimpleTaskDTO(
                    dailyTask.IdTaskNavigation.IdTask,
                    dailyTask.IdTaskNavigation.Name
                ),
                dailyTask.MinutesCompleted,
                dailyTask.TotalMinutes,
                $"{percentage.ToString("00.00")}%"
            );
        }
        public static GetDailyTaskDTO Map(this DailyTask dailyTask)
        {
            return new GetDailyTaskDTO(
                dailyTask.IdDailyTask,
                new SimpleTaskDTO(
                    dailyTask.IdTaskNavigation.IdTask, 
                    dailyTask.IdTaskNavigation.Name
                ),
                dailyTask.MinutesCompleted,
                dailyTask.TotalMinutes,
                (double) (dailyTask.MinutesCompleted / dailyTask.TotalMinutes) * 100,
                dailyTask.CompletedAt
            );
        }
    }
}
