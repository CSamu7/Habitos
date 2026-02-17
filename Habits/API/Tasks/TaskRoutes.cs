using Habits.API.Tasks.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json;

namespace Habits.API.Tasks
{
    public static class TaskRoutes
    {
        public static IEndpointRouteBuilder MapTasks(this IEndpointRouteBuilder builder)
        {
            var tasksRoutes = builder.MapGroup("/tasks");

            tasksRoutes.MapGet("{idTask}/", TasksEndpoints.GetTask)
                .WithName("getTask")
                .Produces<GetTaskResponse>()
                .ProducesProblem(404);

            tasksRoutes.MapPatch("{idTask}/", TasksEndpoints.PatchTask)
                .WithName("patchTask")
                .Accepts<JsonPatchDocument<PostTaskRequest>>("application/json-patch+json")
                .Produces<GetTaskResponse>()
                .ProducesProblem(401)
                .ProducesProblem(404);

            tasksRoutes.MapDelete("{idTask}/", TasksEndpoints.DeleteTask)
                .WithName("deleteTask")
                .Produces(204)
                .ProducesProblem(404);

            return builder;
        }
    }
}
