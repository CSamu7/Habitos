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
        public async static Task<IResult> Register(RegisterUserRequest registerUser, UserManager<User> manager, IUserValidator<User> validator)
        {
            User user = registerUser.ToUser();

            //Check if email or username is unique
            var result = await validator.ValidateAsync(manager, user);

            /*Actually, I shouldn't return a error. I should send an email and apply rate limiting.
            TOOD: But for now it's good for me, in the future I'd like to improve this. */
            if (!result.Succeeded)
                return TypedResults.BadRequest("Email or username is invalid.");

            //Check password
            var creationResult = await manager.CreateAsync(user, registerUser.Password);

            if (!creationResult.Succeeded)
                return TypedResults.ValidationProblem(new Dictionary<string, string[]>()
                {
                    {"Password", creationResult.Errors.Select(x => x.Description).ToArray() }
                });

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
