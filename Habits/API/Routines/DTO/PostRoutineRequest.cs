using Routine = Habits.Models.Routine;

namespace Habits.API.Routines.DTO
{
    public record PostRoutineRequest(string Name, int Minutes, int? IdCategory);
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
            (
                task.Name,
                task.Minutes,
                task.IdCategory
            );
        }
    }
}
