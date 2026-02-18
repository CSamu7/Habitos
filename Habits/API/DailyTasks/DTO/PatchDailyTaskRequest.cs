using Habits.Services.DailyTasks;
using System.ComponentModel.DataAnnotations;

namespace Habits.API.DailyTasks.DTO
{
    public class PatchDailyTaskRequest
    {
        public int Minutes { get; set; }
        public DateTimeOffset FinishedAt { get; set; }
        public PatchOperations Operation { get; set; }
    }
}
