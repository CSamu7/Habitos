namespace Habits.API.Users.DTO
{
    public record LoginUser(string Username, string Password);
    public record RegisterUser(string Username, string Email, string Password);
}
