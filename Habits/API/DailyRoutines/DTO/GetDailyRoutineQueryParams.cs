using Habits.Common.DailyRoutines;

namespace Habits.API.DailyRoutines.DTO
{
    public class GetDailyRoutineQueryParams
    {
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public Progress? Progress { get; set; }
        public GetDailyRoutineQueryParams(DateOnly dateStart, DateOnly dateEnd, Progress? progress)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            Progress = progress;
        }
        public static async ValueTask<GetDailyRoutineQueryParams?> BindAsync(HttpContext context)
        {
            const string dateStartKey = "dateStart";
            const string dateEndKey = "dateEnd";
            const string progressKey = "progress";

            Enum.TryParse(context.Request.Query[progressKey], ignoreCase: true, out Progress progress);

            if (!DateOnly.TryParse(context.Request.Query[dateEndKey], out DateOnly dateEnd))
                dateEnd = DateOnly.FromDateTime(DateTime.UtcNow);

            if (!DateOnly.TryParse(context.Request.Query[dateStartKey], out DateOnly dateStart))
                dateStart = dateEnd.AddDays(-1);

            var result = new GetDailyRoutineQueryParams(dateStart, dateEnd, progress);

            return await ValueTask.FromResult<GetDailyRoutineQueryParams?>(result);
        }
    }

}
