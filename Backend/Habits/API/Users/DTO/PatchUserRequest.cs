namespace Habits.API.Users.DTO
{
    public record PatchUserRequest(string Timezone, int CutOffTime, int MinGoal);
}
