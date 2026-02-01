namespace Habits.API.Tasks
{
    public static class TaskRoutes
    {
        public static IEndpointRouteBuilder MapTasks(this IEndpointRouteBuilder builder)
        {
            var tasksRoute = builder.MapGroup("/tasks");

            return builder;
        }
    }
}
