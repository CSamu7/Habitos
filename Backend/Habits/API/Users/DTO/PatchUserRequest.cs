using Habits.Models;

namespace Habits.API.Users.DTO
{
    public record PatchUserRequest(string? Timezone, int? CutOffTime, int? MinGoal);

    public static class PatchUserRequestExtensions
    {
        public static PatchUserRequest ToPatchRequest(this User user)
        {
            return new(user.TimeZone, user.CutOffTime, user.MinGoal);
        }
    }
}
