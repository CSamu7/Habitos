using Habits.API.Routines.DTO;
using Habits.Models;

namespace Habits.API.DailyRoutines.DTO
{
    public record GetDailyRoutineResponse(
        int IdDailyRoutine,
        GetMinimalRoutineResponse Routine,
        int MinutesCompleted,
        int TotalMinutes,
        string PercentageCompleted,
        DateTimeOffset? CompletedAt
    );

    public static class GetDailyRoutineResponseExtensions
    {
        public static GetDailyRoutineResponse ToGetDailyRoutineResponse(this DailyRoutine dailyRoutine)
        {
            double percentage = (double)dailyRoutine.MinutesCompleted / dailyRoutine.TotalMinutes * 100;
            Routine task = dailyRoutine.IdRoutineNavigation;

            return new GetDailyRoutineResponse(
                dailyRoutine.IdDailyRoutine,
                new GetMinimalRoutineResponse(task.IdRoutine, task.Name),
                dailyRoutine.MinutesCompleted,
                dailyRoutine.TotalMinutes,
                $"{percentage.ToString("00.00")}%",
                dailyRoutine.CompletedAt
            );
        }
    }
}
