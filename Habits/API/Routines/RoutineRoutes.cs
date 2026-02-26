using Habits.API.Routines.DTO;
using Microsoft.AspNetCore.JsonPatch;

namespace Habits.API.Routines
{
    public static class RoutineRoutes
    {
        public static IEndpointRouteBuilder MapRoutines(this IEndpointRouteBuilder builder)
        {
            var tasksRoutes = builder.MapGroup("/routines");

            tasksRoutes.MapGet("{idRoutine}/", RoutineEndpoints.GetRoutine)
                .WithName("getRoutine")
                .Produces<GetRoutineResponse>()
                .ProducesProblem(404);

            tasksRoutes.MapPatch("{idRoutine}/", RoutineEndpoints.PatchRoutine)
                .WithName("patchRoutine")
                .Accepts<JsonPatchDocument<PostRoutineRequest>>("application/json-patch+json")
                .Produces<GetRoutineResponse>()
                .ProducesProblem(401)
                .ProducesProblem(404);

            tasksRoutes.MapDelete("{idRoutine}/", RoutineEndpoints.DeleteTask)
                .WithName("deleteRoutine")
                .Produces(204)
                .ProducesProblem(404);

            return builder;
        }
    }
}
