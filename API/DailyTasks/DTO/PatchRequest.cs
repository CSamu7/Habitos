using Habits.Services.DailyTasks;
using System.ComponentModel.DataAnnotations;

namespace Habits.API.DailyTasks.DTO
{
    public record DailyTaskPatchRequest(
        [Range(1, 480, ErrorMessage = "Los minutos deben ser mayor a 0 y menor a 480")]
        int Minutes,
        DateTimeOffset Time,
        PatchOperations Operation
    );
}
