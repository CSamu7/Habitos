using Habits.Common.DailyTasks;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace Habits.API.DailyTasks.DTO
{
    public record GetDailyTasksQueryParams
    {
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public Progress? Progress { get; set; }
        public GetDailyTasksQueryParams(DateOnly dateStart, DateOnly dateEnd, Progress? progress)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            Progress = progress;
        }
        public static async ValueTask<GetDailyTasksQueryParams?> BindAsync(HttpContext context)
        {
            const string dateStartKey = "dateStart";
            const string dateEndKey = "dateEnd";
            const string progressKey = "progress";

            Enum.TryParse(context.Request.Query[progressKey], ignoreCase: true, out Progress progress);

            if (!DateOnly.TryParse(context.Request.Query[dateEndKey], out DateOnly dateEnd))
                dateEnd = DateOnly.FromDateTime(DateTime.UtcNow);

            if (!DateOnly.TryParse(context.Request.Query[dateStartKey], out DateOnly dateStart))
                dateStart = dateEnd.AddDays(-1);

            var result = new GetDailyTasksQueryParams(dateStart, dateEnd, progress);

            return await ValueTask.FromResult<GetDailyTasksQueryParams?>(result);
        }
    }

}
