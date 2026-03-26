namespace Habits.Common
{
    public interface IBaseListResponse<T>
    {
        public List<T> Results { get; init; }
        public int Count { get; init; }
    }
}
