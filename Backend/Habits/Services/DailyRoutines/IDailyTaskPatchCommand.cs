using Habits.API.DailyRoutines.DTO;
using Habits.Models;

namespace Habits.Services.DailyRoutines
{
    public interface IDailyTaskPatchCommand
    {
        public void ChangeMinutes(DailyRoutine dailyTask, PatchDailyRoutineRequest body);
    }
    public enum PatchOperations { Add, Replace }
}
