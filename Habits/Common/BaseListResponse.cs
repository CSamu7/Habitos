namespace Habits.Common
{
    public record BaseListResponse<T>(List<T> Results, int Count);
}
