using Habits.API.DailyTasks;
using Habits.API.DailyTasks.Validation;
using Habits.API.Tasks;
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
            .AddEndpointFilter<GetAllEndpointFilter>();

            //Nested tasks
            userRoutes.MapPost("/{idUser}/tasks", TasksEndpoints.PostTask);

            return router;
        }
    }
}
