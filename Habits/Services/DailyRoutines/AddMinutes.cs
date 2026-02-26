using Habits.API.DailyRoutines.DTO;
using Habits.Models;

namespace Habits.Services.DailyRoutines
{
    public class AddMinutes : IDailyTaskPatchCommand
    {
        public void ChangeMinutes(DailyRoutine dailyTask, PatchDailyRoutineRequest body)
        {
            dailyTask.MinutesCompleted += body.Minutes;

            if (dailyTask.MinutesCompleted >= dailyTask.TotalMinutes &&
                dailyTask.CompletedAt is null)
                dailyTask.CompletedAt = body.FinishedAt;
        }
    }
}
