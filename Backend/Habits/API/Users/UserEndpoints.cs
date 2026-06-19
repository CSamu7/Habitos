using FluentValidation;
using Habits.API.Users.DTO;
using Habits.Models;
using Habits.Services.Users;
using Microsoft.AspNetCore.Identity;
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
        public async static Task<IResult> ModifyUser(
            [FromBody] JsonElement body, 
            UserService userService, 
            IValidator<PatchUserRequest> bodyValidation, 
            HttpContext ctx)
        {
            PatchUserRequest? bodyDeserialize = body.Deserialize<PatchUserRequest>(new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            if (bodyDeserialize is null) return TypedResults.BadRequest("Hubo un error en el formato del cuerpo");

            string username = ctx.User.Identity?.Name ?? String.Empty;

            var validationResult = bodyValidation.Validate(bodyDeserialize);

            if (!validationResult.IsValid)
                return TypedResults.ValidationProblem(validationResult.ToDictionary());

            var operationResult = await userService.ModifyUser(username, bodyDeserialize);

            if (operationResult.Status != Status.Ok) return TypedResults.BadRequest(operationResult.ErrorMessage);

            return Results.Ok();
        }
        public static IResult DeleteUser()
        {
            return Results.Ok();

        }
    }
}
