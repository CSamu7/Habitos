using Routine = Habits.Models.Routine;

namespace Habits.API.Routines.DTO
{
    public class PostRoutineRequest
    {
        public required string Name { get; set; }
        public int Minutes { get; set; }
        public int? IdCategory { get; set; } = null;
    }

    public static class PostTaskRequestExtensions
    {
        public static Routine ToTask(this PostRoutineRequest postRoutineRequest)
        {
            return new Routine
            {
                Name = postRoutineRequest.Name,
                Minutes = postRoutineRequest.Minutes,
                IdCategory = postRoutineRequest.IdCategory,
                IsActive = true
            };
        }

        public static PostRoutineRequest FromTask(this Routine task)
        {
            return new PostRoutineRequest
            {
                Name = task.Name,
                Minutes = task.Minutes,
                IdCategory = task.IdCategory,
            };
        }
    }
}
