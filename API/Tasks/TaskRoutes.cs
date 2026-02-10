using Habits.API.Tasks.DTO;

namespace Habits.API.Tasks
{
    public static class TaskRoutes
    {
        public static IEndpointRouteBuilder MapTasks(this IEndpointRouteBuilder builder)
        {
            var tasksRoutes = builder.MapGroup("/tasks");

            tasksRoutes.MapGet("{idTask}", TasksEndpoints.GetTask)
                .WithName("getTask")
                .Produces<GetTaskResponse>()
                .ProducesProblem(404);

            tasksRoutes.MapDelete("{idTask}", TasksEndpoints.DeleteTask)
                .WithName("deleteTask")
                .Produces(204)
                .ProducesProblem(404);

            return builder;
        }
    }
}
