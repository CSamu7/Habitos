namespace Habits.Common
{
    public static class DateOnlyExtensions
    {
        public static DateTimeOffset ToDateTimeOffset(this DateOnly date)
            => new DateTimeOffset(date, TimeOnly.FromDateTime(DateTime.UtcNow), new(0));
        public static DateOnly Today(this DateOnly date)
            => DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
