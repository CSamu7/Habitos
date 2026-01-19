using Habits.Features.DailyTasks.Endpoints;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace Habits.Features.Tasks
{
    public class GetAllDailyTaskFilters 
    {
        public DateTimeOffset DateStart { get; init; }
        private readonly TimeProvider _timeProvider;
        public TimeProvider TimeProvider { get; set; } = TimeProvider.System;
        public DateTimeOffset DateEnd { get; init; }
        public GetAllDailyTaskFilters(
            DateOnly? start,
            DateOnly? end, 
            DailyTaskProgress? progress,
            TimeProvider timeProvider
            )
        {
            _timeProvider = timeProvider;

            if (!AreDatesValid(start, end))
                throw new ArgumentException();

            DateEnd = GetDateEnd(end);
            DateStart = GetDateStart(start);

            Progress = progress;
        }
        private DateTimeOffset GetDateStart(DateOnly? date)
        {
            return date is not null
                ? new DateTimeOffset(date.Value, new(0), new(0)).UtcDateTime
                : DateEnd.AddDays(-1).UtcDateTime;
        }
        private DateTimeOffset GetDateEnd(DateOnly? date)
        {
            return date is not null
                ? new DateTimeOffset(date.Value, new TimeOnly(0), new TimeSpan(0)).UtcDateTime
                : TimeProvider.GetUtcNow();
        }
        private bool AreDatesValid(DateOnly? start, DateOnly? end)
        {
            DateOnly today = DateOnly.FromDateTime(TimeProvider.GetUtcNow().Date);

            if (start > today || end > today)
                return false;

            if (start is not null && end is not null)
            {
                if (end < start)
                    return false;
            }

            return true;
        }
        public DailyTaskProgress? Progress { get; set; }
    }

}
