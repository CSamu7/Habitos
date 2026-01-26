using Habits.API.DailyTasks.DTO;
using Habits.Models;

namespace Habits.Services.DailyTasks
{
    public interface IDailyTaskPatchCommand
    {
        public void ChangeMinutes(DailyTask dailyTask, DailyTaskPatchRequest body);
    }
    public enum PatchOperations { Add, Replace }
}
