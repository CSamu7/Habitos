namespace Habits.API.Tasks.DTO
{
    public record GetMinimalTaskResponse(int Id, string Name);
    public record GetTaskResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int? IdGroup { get; set; } = null;
        public GetTaskResponse(Habits.Models.Task task)
        {
            Id = task.IdTask;
            Name = task.Name;
            Minutes = task.Minutes;
            IdGroup = task.IdGroup;
        }
    }
}
