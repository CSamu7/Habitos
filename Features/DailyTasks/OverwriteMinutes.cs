using Habits.Features.Tasks.Models;
using Habits.Models;

namespace Habits.Features.DailyTasks
{
    public class OverwriteMinutes : IDailyTaskPatchCommand
    {
        public void ChangeMinutes(DailyTask dailyTask, DailyTaskPatchRequest body)
        {
            if (body.Minutes < dailyTask.TotalMinutes)
            {
                dailyTask.CompletedAt = null;
            } else
            {
                dailyTask.CompletedAt = body.Time;
            }

            dailyTask.MinutesCompleted = body.Minutes;
        }
    }
}
