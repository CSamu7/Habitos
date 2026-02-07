using Habits.API.Tasks.DTO;

namespace Habits.API.Tasks
{
    public static class TaskRoutes
    {
        public static IEndpointRouteBuilder MapTasks(this IEndpointRouteBuilder builder)
        {
            var tasksRoutes = builder.MapGroup("/tasks");

            tasksRoutes.MapGet("/task/{id}", TasksEndpoints.GetTask)
                .WithName("getTask")
                .Produces<GetTaskResponse>()
                .ProducesProblem(404);

            tasksRoutes.MapDelete("/task/{id}", TasksEndpoints.DeleteTask)
                .WithName("deleteTask")
                .Produces(204)
                .ProducesProblem(404);

            return builder;
        }
    }
}
