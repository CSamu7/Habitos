using Habits.Models;

namespace Habits.API.Users.DTO
{
    public class PatchUserRequest
    {
        public string? Timezone { get; set; }
        public int? CutOffTime { get; set; }
        public int? MinGoal { get; set; }

        public PatchUserRequest(string? timezone, int? cutOffTime, int? minGoal)
        {
            Timezone = timezone;
            CutOffTime = cutOffTime;
            MinGoal = minGoal;
        }
    }

    public static class PatchUserRequestExtensions
    {
        public static PatchUserRequest ToPatchRequest(this User user)
        {
            return new(user.TimeZone, user.CutOffTime, user.MinGoal);
        }
    }
}
