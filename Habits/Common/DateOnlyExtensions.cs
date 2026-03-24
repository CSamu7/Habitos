namespace Habits.Common
{
    public static class DateOnlyExtensions
    {
        public static DateTimeOffset ToDateTimeOffset(this DateOnly date)
            => new DateTimeOffset(date, new(0,0,0), new(0));
        public static DateOnly Today(this DateOnly date)
            => DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
