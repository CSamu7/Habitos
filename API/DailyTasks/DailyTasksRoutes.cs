using Habits.API.DailyTasks.Validation;
using Habits.Features.DailyTasks.Filters;

namespace Habits.API.DailyTasks
{
    public static class DailyTasksRoutes
    {
        public static void UserRoutes(this IEndpointRouteBuilder router)
        {
            DailyTasksEndpoints endpoints = new DailyTasksEndpoints();

            router.MapGet("/{idUser}/dailyTasks/today", endpoints.GetTodayDailyTasks);
            router.MapGet("/{idUser}/dailyTasks", endpoints.GetDailyTasks)
            .AddEndpointFilter<GetAllEndpointFilter>();
        }
        public static void AddDailyTaskRoute(this IEndpointRouteBuilder router)
        {
            DailyTasksEndpoints endpoints = new DailyTasksEndpoints();

            router.MapPatch("{idDailyTask}", endpoints.PatchMinutes)
                .AddEndpointFilter<DailyTaskPatchFilter>();
        }
    }
}
