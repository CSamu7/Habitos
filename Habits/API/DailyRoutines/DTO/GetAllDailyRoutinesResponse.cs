using Habits.Common;
using Habits.Models;

namespace Habits.API.DailyRoutines.DTO
{
    public record GetAllDailyRoutinesResponse
        (
        List<GetDailyRoutineResponse> Results,
        int Count,
        int TotalMinutes,
        int MinutesCompleted,
        int MinutesLeft,
        string PercentageCompleted
        ) : IBaseListResponse<GetDailyRoutineResponse>;

    public static class GetAllDailyRoutinesResponseExtensions
    {
        public static GetAllDailyRoutinesResponse ToGetAllDailyRoutinesResponse(this List<DailyRoutine> list)
        {
            int minutesCompleted = list.Aggregate(0, (acc, task) => acc += task.MinutesCompleted);
            int totalMinutes = list.Aggregate(0, (acc, task) => acc += task.TotalMinutes);
            double percentage = (double)minutesCompleted / totalMinutes * 100;

            return new GetAllDailyRoutinesResponse(
                list.Select(d => d.ToGetDailyRoutineResponse()).ToList(),
                list.Count,
                totalMinutes,
                minutesCompleted,
                totalMinutes - minutesCompleted,
                $"{percentage.ToString("00.00")}%"

            );
        }
    }
}
