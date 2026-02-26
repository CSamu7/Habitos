using Habits.API.Routines.DTO;
using Habits.Models;

namespace Habits.API.DailyRoutines.DTO
{
    public class GetDailyRoutineResponse
    {
        public int IdDailyRoutine { get; init; }
        public required GetMinimalRoutineResponse SimpleTask { get; init; }
        public int MinutesCompleted { get; init; }
        public int TotalMinutes { get; init; }
        public double PercentageCompleted { get; init; }
        public DateTimeOffset? CompletedAt { get; init; }
        public static GetDailyRoutineResponse FromDailyTask(DailyRoutine dailyTask)
        {
            double percentage = (double)dailyTask.MinutesCompleted / dailyTask.TotalMinutes * 100;
            Habits.Models.Routine task = dailyTask.IdRoutineNavigation;

            return new GetDailyRoutineResponse
            {
                IdDailyRoutine = dailyTask.IdDailyRoutine,
                SimpleTask = new GetMinimalRoutineResponse(dailyTask.IdRoutine, dailyTask.IdRoutineNavigation.Name),
                MinutesCompleted = dailyTask.MinutesCompleted,
                TotalMinutes = dailyTask.TotalMinutes,
                PercentageCompleted = percentage,
                CompletedAt = dailyTask.CompletedAt
            };
        }
    }
}
