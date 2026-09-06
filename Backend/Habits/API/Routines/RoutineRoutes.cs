using Habits.API.Policies;
using Habits.API.Routines.DTO;
using Habits.API.Routines.Filters;
using Microsoft.AspNetCore.JsonPatch;

namespace Habits.API.Routines
{
    public static class RoutineRoutes
    {
        public static IEndpointRouteBuilder MapRoutines(this IEndpointRouteBuilder builder)
        {
            var routineRoutes = builder.MapGroup("/routines");
            var userRoutes = builder.MapGroup("users/{username}");

            routineRoutes.AddEndpointFilter<IsSameRoutineOwnerFilter>();

            userRoutes.MapGet("/routines", RoutineEndpoints.GetAllRoutines)
                .AddEndpointFilter<IsOwnerFilter>()
                .WithName("getAllRoutines")
                .Produces<List<GetRoutineResponse>>(200);

            routineRoutes.MapGet("{idRoutine}/", RoutineEndpoints.GetRoutine)
                .WithName("getRoutine")
                .Produces<GetRoutineResponse>()
                .ProducesProblem(404);

            userRoutes.MapPost("/routines", RoutineEndpoints.PostRoutine)
                .AddEndpointFilter<IsOwnerFilter>()
                .AddEndpointFilter<PostRoutineFilter>();

            routineRoutes.MapPatch("{idRoutine}/", RoutineEndpoints.PatchRoutine)
                .WithName("patchRoutine")
                .Accepts<JsonPatchDocument<PostRoutineRequest>>("application/json-patch+json")
                .Produces<GetRoutineResponse>()
                .ProducesProblem(401)
                .ProducesProblem(404);

            routineRoutes.MapDelete("{idRoutine}/", RoutineEndpoints.DeleteTask)
                .WithName("deleteRoutine")
                .Produces(204)
                .ProducesProblem(404);

            return builder;
        }
    }
}
