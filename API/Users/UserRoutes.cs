using Habits.API.DailyTasks;
using Habits.API.DailyTasks.Validation;
using Habits.API.Tasks;
using Habits.API.Tasks.DTO;
using Habits.API.Tasks.Filters;
using Microsoft.AspNetCore.Components.Routing;

namespace Habits.API.Users
{
    public static class UserRoutes
    {
        public static IEndpointRouteBuilder MapUsers(this IEndpointRouteBuilder router)
        {
            var userRoutes = router.MapGroup("/users");

            //Nested dailyTasks
            userRoutes.MapGet("/{idUser}/dailyTasks/today", DailyTasksEndpoints.GetDailyTasks);
            userRoutes.MapGet("/{idUser}/dailyTasks", DailyTasksEndpoints.GetDailyTasks)
            .AddEndpointFilter<GetAllDailyTasksEndpointFilter>();

            //Nested tasks
            userRoutes.MapPost("/{idUser}/tasks", TasksEndpoints.PostTask)
                .AddEndpointFilter<PostTaskFilter>();

            userRoutes.MapGet("/{idUser}/tasks", TasksEndpoints.GetAllTasks)
                .WithName("getAllTasks")
                .Produces<List<GetTaskResponse>>(200);

            return router;
        }
    }
}
