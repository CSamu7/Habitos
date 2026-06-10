using Habits.API.Users.DTO;
using Habits.Common;
using Habits.Models;
using Habits.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Habits.API.Users
{
    public class UserEndpoints
    {
        public static async Task<IResult> GetUser(UserManager<User> userManager, HttpContext ctx)
        {
            ClaimsPrincipal claims = ctx.User;

            string? username = claims?.Identity?.Name;

            if (username is null)
                return TypedResults.Problem("No tienes acceso a este recurso", statusCode: 401);

            User? user = await userManager.FindByNameAsync(username);

            if (user is null)
                return TypedResults.Problem("No tienes acceso a este recurso", statusCode: 401);

            GetUserResponse response = user.ToGetResponse();

            return Results.Ok(response);
        }
        public static async Task<IResult> Login(LoginUser loginUser, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            var result = await signInManager.PasswordSignInAsync(loginUser.Username, loginUser.Password, false, false);

            if (!result.Succeeded)
                return TypedResults.Problem("Nombre de usuario o contraseña incorrectos", statusCode: 401);

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
                    .ToDictionary(e => e.Key.ToLower(), e => e.Select(info => info.Description).ToArray());

                return TypedResults.ValidationProblem(errors);
            }

            return Results.Created();
        }
        public async static Task<IResult> ModifyUser([FromBody] JsonElement jsonElement, UserService userService)
        {
            var doc = jsonElement.ToPatchDocument();

            if (doc is null) return Results.BadRequest();

            try
            {
                userService.ModifyUser(doc);
                return Results.Ok();
            }
            catch (JsonPatchException ex)
            {
                return Results.Problem(ex.Message, statusCode: 400);
            }
        }
        public static IResult DeleteUser()
        {
            return Results.Ok();

        }
    }
}
