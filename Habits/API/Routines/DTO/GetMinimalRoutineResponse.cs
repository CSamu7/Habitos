namespace Habits.API.Routines.DTO
{
    public record GetMinimalRoutineResponse(int Id, string Name);
    public record GetRoutineResponse(int Id, string Name, int Minutes, int? IdCategory);
}
