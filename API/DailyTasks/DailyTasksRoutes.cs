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

            dailyTasksRoute.MapGet("{idDailyTask}", DailyTasksEndpoints.GetDailyTask)
                .Produces(200)
                .ProducesProblem(404);

            dailyTasksRoute.MapPatch("{idDailyTask}", DailyTasksEndpoints.PatchMinutes)
                .WithName("patchDailyTask")
                .Produces(204)
                .ProducesProblem(401)
                .ProducesProblem(404)
                .AddEndpointFilter<DailyTaskPatchFilter>();

            return router;
        }
    }
}
