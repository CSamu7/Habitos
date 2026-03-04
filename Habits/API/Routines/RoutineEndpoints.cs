using Habits.API.Policies;
using Habits.API.Routines.DTO;
using Habits.Common;
using Habits.Models;
using Habits.Services.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Habits.API.Routines
{
    public static class RoutineEndpoints
    {
        public static async Task<IResult> GetRoutine(int idRoutine, RoutineService service, IAuthorizationService authService, HttpContext ctx)
        {
            Result<Routine> result = await service.GetRoutine(idRoutine);

            var authResult = await authService.AuthorizeAsync(ctx.User, result.Value, new RoutineOwnerRequirement());

            if (!authResult.Succeeded)
                return TypedResults.Unauthorized();

            if (result.Status.Equals(Status.Ok))
            {
                GetRoutineResponse response = new GetRoutineResponse(result.Value);
                return TypedResults.Ok<GetRoutineResponse>(response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PostRoutine(string idUser, PostRoutineRequest body, LinkGenerator generator, RoutineService service)
        {
            Result<Routine> result = await service.PostRoutine(idUser, body);

            if (result.Status.Equals(Status.Ok))
            {
                GetRoutineResponse response = new GetRoutineResponse(result.Value);
                string? uri = generator.GetPathByName
                    ("getTask", new() { { "idTask", response.Id } });

                return TypedResults.Created<GetRoutineResponse>(uri, response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PatchRoutine(int idRoutine, [FromBody] JsonElement jsonElement, RoutineService service, HttpRequest httpRequest)
        {
            var json = jsonElement.GetRawText();
            var doc = JsonConvert.DeserializeObject<JsonPatchDocument>(json);

            var newDoc = doc?.Sanitize();

            try
            {
                await service.PatchTask(idRoutine, newDoc);
                return Results.Ok();
            } catch (JsonPatchException ex)
            {
                return Results.Problem(ex.Message, statusCode: 400);
            }
        }
        public static async Task<IResult> GetAllRoutines(string idUser, RoutineService service)
        {
            Result<List<Routine>> result = await service.GetAllRoutines(idUser);

            if (result.Status.Equals(Status.Ok))
            {
                List<GetRoutineResponse> tasks = result.Value.Select(task => new GetRoutineResponse(task)).ToList();
                return TypedResults.Ok<List<GetRoutineResponse>>(tasks);
            }

            return result.ToHttpResponse();
        }

        public static async Task<IResult> DeleteTask(int idRoutine, RoutineService service)
        {
            Result<Routine> result = await service.DeleteTask(idRoutine);

            if (result.Status.Equals(Status.Ok))
                return TypedResults.NoContent();

            return Results.Ok();
        }
    }
}
