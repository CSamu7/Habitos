using Habits.Models;
using Habits.Common;

namespace Habits.API.DailyTasks.DTO
{
    public record GetAllResponse : ResponseBase<DailyTaskGetResponse>
    {
        public int TotalMinutes { get; set; } = 0;
        public int MinutesCompleted { get; set; } = 0;
        public int MinutesLeft { get; set; } = 0;
        public string PercentageCompleted { get; set; } = "0.00%";
        public GetAllResponse(List<DailyTask> results) :
            base(results.Select(res => res.ToGetResponse()).ToList(), results.Count)
        {
            TotalMinutes = GetTotalMinutes();
            MinutesCompleted = GetMinutesCompleted();
            MinutesLeft = TotalMinutes - MinutesCompleted;
            PercentageCompleted = GetTotalPercentage();
        }
        public int GetMinutesCompleted() =>
            Results.Aggregate(0, (acc, task) => acc += task.MinutesCompleted);
        public int GetTotalMinutes() =>
            Results.Aggregate(0, (acc, task) => acc += task.TotalMinutes);
        public string GetTotalPercentage()
        {
            double percentage = (double)GetMinutesCompleted() / GetTotalMinutes() * 100;

            return double.IsFinite(percentage)
                ? $"{percentage.ToString("00.00")}%"
                : "0.00%";
        }
    }
}
