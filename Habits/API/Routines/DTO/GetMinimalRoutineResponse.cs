using Habits.Models;

namespace Habits.API.Routines.DTO
{
    public record GetMinimalRoutineResponse(int Id, string Name);
    public record GetRoutineResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int? IdCategory { get; set; } = null;
        public GetRoutineResponse(Routine task)
        {
            Id = task.IdRoutine;
            Name = task.Name;
            Minutes = task.Minutes;
            IdCategory = task.IdCategory;
        }
    }
}
