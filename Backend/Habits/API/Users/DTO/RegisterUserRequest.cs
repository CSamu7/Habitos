using Habits.Models;

namespace Habits.API.Users.DTO
{
    public record RegisterUserRequest(
        string Username,
        string Email,
        string Password
    );
    public static class RegisterUserExtensions
    {
        public static User ToUser(this RegisterUserRequest user)
        {
            return new User
            {
                UserName = user.Username,
                Email = user.Email,
                MinGoal = 75,
                CutOffTime = 0,
                TimeZone = "UTC"
            };
        }
    }
}
