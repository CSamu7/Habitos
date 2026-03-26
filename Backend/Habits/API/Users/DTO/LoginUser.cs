using Habits.Models;

namespace Habits.API.Users.DTO
{
    public record LoginUser(string Username, string Password);

    public static class LoginUserExtensions
    {
        public static User ToUser(this LoginUser loginUser)
        {
            return new User
            {
                UserName = loginUser.Username,
            };
        }
    }
}
