using Habits.API.Users.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Habits.API.Users
{
    public class UserEndpoints
    {
        public static async Task<IResult> GetUser(int idUser)
        {
            return Results.Ok();
        }
        public static IResult Login([FromBody] LoginUser login)
        {
            return Results.Ok();
        }
        public static IResult Register(RegisterUser registerUser)
        {
            return Results.Ok();

        }
        public static IResult ModifyUser()
        {
            return Results.Ok();

        }
        public static IResult DeleteUser()
        {
            return Results.Ok();

        }
    }
}
