using Habits.API.DailyRoutines.DTO;
using Habits.API.DailyRoutines.Filters;
using Habits.API.DailyRoutines.Validation;
using Habits.API.Policies;
using Habits.Common;
using Habits.Common.DailyRoutines;
using Habits.Features.DailyTasks.Filters;

namespace Habits.API.DailyRoutines
{
    public static class DailyRoutineRoutes
    {
        public static IEndpointRouteBuilder MapDailyRoutines(this IEndpointRouteBuilder router)
        {
            var dailyTasksRoute = router.MapGroup("/dailyRoutines")
                .AddEndpointFilter<DailyRoutineOwnerFilter>();

            var userRoutes = router.MapGroup("users/{username}");

            //Nested dailyRoutines
            userRoutes.MapGet("/dailyRoutines/today", async (string username, DailyRoutineService service) =>
            {
                DateOnly dateEnd = DateOnly.FromDateTime(DateTime.UtcNow);
                DateOnly dateStart = dateEnd.AddDays(-1);

                return await DailyRoutineEndpoints.GetDailyRoutines
                (username, new(dateStart.ToDateTimeOffset(), dateEnd.ToDateTimeOffset(), [Progress.NotDone, Progress.InProgress, Progress.Done]), service);
            }).AddEndpointFilter<IsOwnerFilter>();

            userRoutes.MapGet("/dailyRoutines", DailyRoutineEndpoints.GetDailyRoutines)
                .AddEndpointFilter<IsOwnerFilter>()
                .AddEndpointFilter<GetAllDailyRoutinesEndpointFilter>()
                .WithDescription("""
                    NotDone: Task that is available and it hasn't been started
                    InProgress: Task that is available and it has been started but not completed
                    Done: Task completed
                    Incomplete: Task that is not available and it wasn't completed.
                    Overdone: Task where the used overpassed the time limit.
                """);

            dailyTasksRoute.MapGet("/{idDailyRoutine}", DailyRoutineEndpoints.GetDailyRoutine)
                .Produces<GetDailyRoutineResponse>(200)
                .ProducesProblem(404);

            dailyTasksRoute.MapPatch("/{idDailyRoutine}", DailyRoutineEndpoints.PatchMinutes)
                .WithName("patchDailyRoutine")
                .Produces(204)
                .ProducesProblem(401)
                .ProducesProblem(404)
                .AddEndpointFilter<DailyRoutinePatchFilter>();

            return router;
        }
    }
}
