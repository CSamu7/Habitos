using Habits.Models;

namespace Habits.API.Users.DTO
{
    public record RegisterUserRequest(
        string Username,
        string Email,
        string Password,
        byte MinGoal,
        DateTimeOffset CutOffTime
    );
    public static class RegisterUserExtensions
    {
        public static User ToUser(this RegisterUserRequest user)
        {
            return new User
            {
                UserName = user.Username,
                Email = user.Email,
                MinGoal = user.MinGoal,
                CutOffTime = user.CutOffTime,
            };
        }
    }
}
