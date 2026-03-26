using Habits.Services.DailyRoutines;

namespace Habits.API.DailyRoutines.DTO
{
    public record PatchDailyRoutineRequest(int Minutes, DateTimeOffset FinishedAt, PatchOperations Operation);
}
