namespace Habits.Common
{
    public interface IBaseListResponse<T>
    {
        public List<T> List { get; init; }
        public int Count { get; init; }
    }
}
