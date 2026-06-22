using Habits.API.Routines.DTO;
using Habits.Common.DailyRoutines;
using Habits.Features.DailyTasks.Models;
using Habits.Models;

namespace Habits.API.DailyRoutines.DTO
{
    public record GetDailyRoutineResponse(
        int IdDailyRoutine,
        GetMinimalRoutineResponse Routine,
        int MinutesCompleted,
        int TotalMinutes,
        string PercentageCompleted,
        string Status,
        DateTimeOffset? CompletedAt
    );

    public static class GetDailyRoutineResponseExtensions
    {
        public static GetDailyRoutineResponse ToGetDailyRoutineResponse(this DailyTask dailyRoutine)
        {
            double percentage = (double)dailyRoutine.MinutesCompleted / dailyRoutine.TotalMinutes * 100;
            Routine task = dailyRoutine.IdRoutineNavigation;

            Progress status = dailyRoutine.GetProgress();

            return new GetDailyRoutineResponse(
                dailyRoutine.IdDailyTask,
                new GetMinimalRoutineResponse(task.IdRoutine, task.Name),
                dailyRoutine.MinutesCompleted,
                dailyRoutine.TotalMinutes,
                $"{percentage.ToString("00.00")}%",
                status.ToString(),
                dailyRoutine.CompletedAt
            );
        }
    }
}
