using Habits.API.DailyTasks;

namespace Habits.API
{
    public static class HabitsEndpoints
    {
        public static void UseHabitsEndpoints(this WebApplication app)
        {
            
            IEndpointRouteBuilder api = app.MapGroup("/api/");
            IEndpointRouteBuilder users = api.MapGroup("/users/");
            IEndpointRouteBuilder dailyTasks = api.MapGroup("/dailyTasks/");

            users.UserRoutes();
            dailyTasks.AddDailyTaskRoute();
        }
    }
}
