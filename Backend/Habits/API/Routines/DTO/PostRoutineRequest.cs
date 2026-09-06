namespace Habits.API.Routines.DTO
{
    public record PostRoutineRequest(string Name, int Minutes, List<int> Categories);

}
