using Habits.API.DailyTasks.Validation;
using Habits.API.Tasks;
using Habits.Features.DailyTasks.Filters;

namespace Habits.API.DailyTasks
{
    //Change name to HabitsRoutes?
    public static class DailyTasksRoutes
    {
        public static IEndpointRouteBuilder MapDailyTasks(this IEndpointRouteBuilder router)
        {
            var dailyTasksRoute = router.MapGroup("/dailyTasks");

            dailyTasksRoute.MapGet("{idDailyTask}", DailyTasksEndpoints.GetDailyTask);

            dailyTasksRoute.MapPatch("{idDailyTask}", DailyTasksEndpoints.PatchMinutes)
                .AddEndpointFilter<DailyTaskPatchFilter>();

            return router;
        }
    }
}
