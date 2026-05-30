using Habits.API.Users.DTO;
using Habits.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Habits.API.Users
{
    public class UserEndpoints
    {
        public static async Task<IResult> GetUser(UserManager<User> userManager, HttpContext ctx)
        {
            ClaimsPrincipal claims = ctx.User;

            string? username = claims?.Identity?.Name;

            if (username is null)
                return TypedResults.Problem("You have to login", statusCode: 401);

            User? user = await userManager.FindByNameAsync(username);

            if (user is null)
                return TypedResults.Problem("There's some problem with the user", statusCode: 401);

            GetUserResponse response = user.ToGetResponse();

            return Results.Ok(response);
        }
        public static async Task<IResult> Login(LoginUser loginUser, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            var result = await signInManager.PasswordSignInAsync(loginUser.Username, loginUser.Password, false, false);

            if (!result.Succeeded)
                return TypedResults.Problem("Invalid username or password", statusCode: 401);

            return Results.Ok();
        }
        public async static Task<IResult> Register(RegisterUserRequest registerUser, UserManager<User> manager)
        {
            User user = registerUser.ToUser();

            var result = await manager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
            {
                Dictionary<string, string[]> errors = result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(e => e.Key, e => e.Select(info => info.Description).ToArray());

                return TypedResults.BadRequest(errors);
            }

            return Results.Created();
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
