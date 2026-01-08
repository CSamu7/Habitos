using Habits.Features.Tasks.Models;
using Habits.Models;

namespace Habits.Features.DailyTasks
{
    public class AddMinutes : IDailyTaskPatchCommand
    {
        public void ChangeMinutes(DailyTask dailyTask, DailyTaskPatchRequest body)
        {
            dailyTask.MinutesCompleted += body.Minutes;

            if (dailyTask.MinutesCompleted >= dailyTask.TotalMinutes &&
                dailyTask.CompletedAt is null)
                dailyTask.CompletedAt = body.Time;
        }
    }
}
