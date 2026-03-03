using Habits.Models;

namespace Habits.API.Users.DTO
{
    public record GetUserResponse(
        string Username, 
        byte MinGoal, 
        DateTimeOffset CutOffTime, 
        int Streak);

    public static class GetUserResponseExtensions
    {
        public static GetUserResponse ToGetResponse(this User user)
        {
            return new(user.UserName, user.MinGoal, user.CutOffTime, user.Streak);
        }
    }
}
