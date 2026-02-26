using Habits.Features.DailyTasks.Filters;

namespace Habits.API.DailyRoutines
{
    public static class DailyRoutineRoutes
    {
        public static IEndpointRouteBuilder MapDailyRoutines(this IEndpointRouteBuilder router)
        {
            var dailyTasksRoute = router.MapGroup("/dailyRoutines");

            dailyTasksRoute.MapGet("{idDailyRoutine}", DailyRoutineEndpoints.GetDailyRoutine)
                .Produces(200)
                .ProducesProblem(404);

            dailyTasksRoute.MapPatch("{idDailyRoutine}", DailyRoutineEndpoints.PatchMinutes)
                .WithName("patchDailyRoutine")
                .Produces(204)
                .ProducesProblem(401)
                .ProducesProblem(404)
                .AddEndpointFilter<DailyRoutinePatchFilter>();

            return router;
        }
    }
}
