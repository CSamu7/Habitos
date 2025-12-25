namespace Habits.Features.Tasks
{
    public static class DailyTasksRoutes
    {
        public static void AddUserRoute(this IEndpointRouteBuilder router)
        {
            DailyTasksEndpoints endpoints = new DailyTasksEndpoints();

            router.MapGet("/{idUser}/dailyTasks/today", endpoints.GetTodayDailyTasks);
            router.MapGet("/{idUser}/dailyTasks", endpoints.GetDailyTasks);
        }
        public class DailyTaskFilters() : IEndpointFilter
        {
            public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
            {
                var body = context.GetArgument<PatchDailyTask>(1);

                DailyTaskFilter validator = new DailyTaskFilter();
                Dictionary<string, string[]> errors = validator.Validate(body);

                if (errors.Count > 0)
                    return TypedResults.ValidationProblem(
                        errors
                    );

                return await next(context);
            }
        }
        public static void AddDailyTaskRoute(this IEndpointRouteBuilder router)
        {
            DailyTasksEndpoints endpoints = new DailyTasksEndpoints();

            router.MapPatch("{idDailyTask}", endpoints.PatchMinutes)
                .AddEndpointFilter<DailyTaskFilters>();
        }
    }
}
