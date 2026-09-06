using Habits.API.Categories.DTO;
using Habits.Models;

namespace Habits.API.Routines.DTO
{
    public record GetMinimalRoutineResponse(int Id, string Name);
    public record GetRoutineResponse(int Id, string Name, int Minutes, ICollection<MinimalCategory> Categories);

    public static class GetRoutineResponseExtensions
    {
        public static GetRoutineResponse ToGetRoutineResponse(this Routine routine)
        {
            return new GetRoutineResponse(
                routine.IdRoutine,
                routine.Name, 
                routine.Minutes,
                routine.Categories.Select(c => new MinimalCategory(c.IdCategory, c.Name, c.Color)).ToList()
            );
        }
    }
}
