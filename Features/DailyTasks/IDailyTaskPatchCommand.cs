using Habits.Features.Tasks.Models;
using Habits.Models;

namespace Habits.Features.DailyTasks
{
    public interface IDailyTaskPatchCommand
    {
        public void ChangeMinutes(DailyTask dailyTask, DailyTaskPatchRequest body);
    }
    public enum PatchOperations { Add, Replace }
}
