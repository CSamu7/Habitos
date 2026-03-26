using Habits.API.DailyRoutines.DTO;
using Habits.Models;

namespace Habits.Services.DailyRoutines
{
    public class OverwriteMinutes : IDailyTaskPatchCommand
    {
        public void ChangeMinutes(DailyRoutine dailyTask, PatchDailyRoutineRequest body)
        {
            if (body.Minutes < dailyTask.TotalMinutes)
            {
                dailyTask.CompletedAt = null;
            } else
            {
                dailyTask.CompletedAt = body.FinishedAt;
            }

            dailyTask.MinutesCompleted = body.Minutes;
        }
    }
}
