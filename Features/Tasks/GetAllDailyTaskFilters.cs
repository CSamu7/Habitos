namespace Habits.Features.Tasks
{
    public class GetAllDailyTaskFilters
    {
        public DateTimeOffset DateStart { get; init; }
        public DateTimeOffset DateEnd { get; init; }
        public GetAllDailyTaskFilters(DateOnly? start, DateOnly? end, DailyTaskProgress? progress)
        {
            DateStart = GetDateStart(start);
            DateEnd = GetDateEnd(end);
            Progress = progress;
        }
        private DateTimeOffset GetDateStart(DateOnly? date)
        {
            return date is not null
                ? new DateTimeOffset(date.Value, new TimeOnly(0), new TimeSpan(0)).UtcDateTime
                : DateTimeOffset.UtcNow.Subtract(new TimeSpan(1, 0, 0, 0));
        }
        private DateTimeOffset GetDateEnd(DateOnly? date)
        {
            return date is not null
                ? new DateTimeOffset(date.Value, new TimeOnly(23, 59, 59), new TimeSpan(0)).UtcDateTime
                : DateTimeOffset.UtcNow;
        }
        public DailyTaskProgress? Progress { get; set; }
    }

}
