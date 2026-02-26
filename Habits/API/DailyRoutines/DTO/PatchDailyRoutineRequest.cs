using Habits.Services.DailyRoutines;

namespace Habits.API.DailyRoutines.DTO
{
    public class PatchDailyRoutineRequest
    {
        public int Minutes { get; set; }
        public DateTimeOffset FinishedAt { get; set; }
        public PatchOperations Operation { get; set; }
    }
}
