using Habits.Common;
using Habits.Common.DailyRoutines;

namespace Habits.API.DailyRoutines.DTO
{
    public class GetDailyRoutineQueryParams
    {
        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset DateEnd { get; set; }
        public Progress[] Progress { get; set; }
        public GetDailyRoutineQueryParams(DateTimeOffset dateStart, DateTimeOffset dateEnd, Progress[] progress)
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

            string? progressParams = context.Request.Query[progressKey];
            string[] progressList = progressParams?.Split(",") ?? [];
            List<Progress> finalProgress = [];

            foreach(string strProgress in progressList)
            {
                Enum.TryParse(strProgress, ignoreCase: true, out Progress progress);
                finalProgress.Add(progress);
            }

            if (!DateOnly.TryParse(context.Request.Query[dateEndKey], out DateOnly dateEnd))
                dateEnd = DateOnly.FromDateTime(DateTime.UtcNow);

            if (!DateOnly.TryParse(context.Request.Query[dateStartKey], out DateOnly dateStart))
                dateStart = dateEnd.AddDays(-1);

            var result = new GetDailyRoutineQueryParams(dateStart.ToDateTimeOffset(), dateEnd.ToDateTimeOffset(), finalProgress.ToArray());

            return await ValueTask.FromResult<GetDailyRoutineQueryParams?>(result);
        }
    }

}
