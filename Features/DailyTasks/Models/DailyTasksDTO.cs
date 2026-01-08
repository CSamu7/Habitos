using Habits.Features.DailyTasks;
using System.ComponentModel.DataAnnotations;

namespace Habits.Features.Tasks.Models
{
    public record DailyTaskGetResponse(
        int IdDailyTask,
        SimpleTaskDTO Task,
        int MinutesCompleted,
        int TotalMinutes,
        double PercentageCompleted,
        DateTimeOffset? CompletedAt
    );
    ///???
    public record DailyTaskStatsResponse(
        int IdDailyTask,
        SimpleTaskDTO Task,
        int MinutesCompleted, 
        int TotalMinutes,
        string PercentageCompleted
    );
    public record DailyTaskPatchRequest(
        [Range(1, 480, ErrorMessage = "Los minutos deben ser mayores a 0 y menores a 480")]
        int Minutes,
        DateTimeOffset Time,
        PatchOperations Operation
    );
}
