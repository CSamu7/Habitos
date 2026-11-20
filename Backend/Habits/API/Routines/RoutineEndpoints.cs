using Habits.API.Routines.DTO;
using Habits.Models;
using Habits.Services.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;

namespace Habits.API.Routines
{
    public static class RoutineEndpoints
    {
        //TODO: Configurar para que no se retorne el id de usuario en la coleccion de categorias.
        public static async Task<IResult> GetRoutine(int idRoutine, RoutineService service)
        {
            Result<Routine> result = await service.Get(idRoutine);

            if (result.Status.Equals(Status.Ok))
            {
                Routine routine = result.Value;

                var response = routine.ToGetRoutineResponse();
                return TypedResults.Ok(response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PostRoutine(string username, PostRoutineRequest body, LinkGenerator generator, RoutineService service, UserManager<User> manager, HttpContext ctx)
        {
            var user = await manager.GetUserAsync(ctx.User);

            if (user is null || user.UserName != username) return TypedResults.Unauthorized();

            Result<Routine> result = await service.Post(user.Id, body);

            if (result.Status.Equals(Status.Ok))
            {
                Routine routine = result.Value;

                var response = routine.ToGetRoutineResponse();
                string? uri = generator.GetPathByName
                    ("getRoutine", new() { { "idRoutine", response.Id } });

                return TypedResults.Created(uri, response);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> GetAllRoutines(string username, RoutineService service, UserManager<User> userManager, HttpContext ctx)
        {
            var user = await userManager.GetUserAsync(ctx.User);

            if (user is null || user.UserName != username) return TypedResults.Unauthorized();
            Result<List<Routine>> result = await service.GetAll(user.Id);

            if (result.Status.Equals(Status.Ok))
            {
                List<GetRoutineResponse> routines = result.Value
                    .Select(routine => routine.ToGetRoutineResponse())
                    .ToList();

                return TypedResults.Ok(routines);
            }

            return result.ToHttpResponse();
        }
        public static async Task<IResult> PatchRoutine(
            int idRoutine,
            JsonPatchDocument<PatchRoutineRequest> body,
            RoutineService service
            )
        {
            var result = await service.Patch(idRoutine, body);

            if (result.Status.Equals(Status.Ok))
                return Results.NoContent();

            return result.ToHttpResponse();
        }

        public static async Task<IResult> DeleteTask(int idRoutine, RoutineService service)
        {
            Result<Routine> result = await service.Delete(idRoutine);

            return Results.NoContent();
        }
    }
}
