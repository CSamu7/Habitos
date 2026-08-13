using Habits.API.Routines.DTO;
using Habits.Common;
using Habits.Models;
using Habits.Services.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Habits.API.Routines
{
    public static class RoutineEndpoints
    {
        public static async Task<IResult> GetRoutine(int idRoutine, RoutineService service)
        {
            Result<Routine> result = await service.GetRoutine(idRoutine);

            if (result.Status.Equals(Status.Ok))
            {
                Routine routine = result.Value;

                var response = new GetRoutineResponse
                    (routine.IdRoutine, routine.Name, routine.Minutes, routine.IdRoutineCategory, 
                    routine.IdRoutineCategoryNavigation?.Color.ToString() ?? "FFF");
                return TypedResults.Ok(response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PostRoutine(string username, PostRoutineRequest body, LinkGenerator generator, RoutineService service, UserManager<User> manager, HttpContext ctx)
        {
            var user = ctx.User;

            if (user is null) return TypedResults.Unauthorized();

            //TODO: Checar que el nombre en el endpoint y del usuario sea el mismo.

            var idUser = user.Claims.FirstOrDefault(x => x.Type == "idUser")?.Value;

            if (idUser is null)
            {
                Console.WriteLine("¿SIN CLAIM?");
                return TypedResults.NotFound();
            }

            Result<Routine> result = await service.PostRoutine(idUser, body);

            if (result.Status.Equals(Status.Ok))
            {
                Routine routine = result.Value;

                var response = new GetRoutineResponse(routine.IdRoutine, routine.Name, routine.Minutes, routine.IdRoutineCategory, routine.IdRoutineCategoryNavigation?.Color.ToString() ?? "");
                string? uri = generator.GetPathByName
                    ("getRoutine", new() { { "idRoutine", response.Id } });

                return TypedResults.Created(uri, response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> GetAllRoutines(string username, RoutineService service, UserManager<User> userManager, HttpContext ctx)
        {
            var user = await userManager.GetUserAsync(ctx.User);

            if (user is null) return TypedResults.Unauthorized();
            Result<List<Routine>> result = await service.GetAllRoutines(user.Id);

            if (result.Status.Equals(Status.Ok))
            {
                List<GetRoutineResponse> tasks = result.Value
                    .Select(task => new GetRoutineResponse(task.IdRoutine, task.Name, task.Minutes, task.IdRoutineCategory, task?.IdRoutineCategoryNavigation?.Color ?? "#333"))
                    .ToList();
                    
                return TypedResults.Ok(tasks);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PatchRoutine(
            int idRoutine,
            [FromBody] JsonElement jsonElement,
            RoutineService service)
        {
            var doc = jsonElement.ToPatchDocument();

            if (doc is null) return Results.BadRequest();

            try
            {
                var result = await service.PatchTask(idRoutine, doc);
                return Results.Ok();
            } catch (JsonPatchException ex)
            {
                return Results.Problem(ex.Message, statusCode: 400);
            }
        }

        public static async Task<IResult> DeleteTask(int idRoutine, RoutineService service)
        {
            Result<Routine> result = await service.DeleteTask(idRoutine);

            return Results.NoContent();
        }
    }
}
