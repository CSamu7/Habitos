using Habits.API.DailyTasks.DTO;

namespace Habits.Features.DailyTasks.Filters
{
    public class DailyTaskPatchFilter() : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var body = context.GetArgument<PatchDailyTaskRequest>(1);

            Dictionary<string, string[]> errors = Validate(body);

            if (errors.Count > 0)
                return TypedResults.ValidationProblem(
                    errors
                );

            return await next(context);
        }
        private Dictionary<string, string[]> Validate(PatchDailyTaskRequest body)
        {
            Dictionary<string, string[]> dicErrors = new();
            int maxMinutes = 480; // 8 horas

            List<string> minutesErrors = new List<string>();

            if (body.Minutes >= maxMinutes)
                minutesErrors.Add("Los minutos no pueden ser mayor a 8 horas");

            if (body.Minutes <= 0)
                minutesErrors.Add("Los minutos deben ser positivos");

            if (minutesErrors.Count > 0)
                dicErrors.TryAdd("Minutes", minutesErrors.ToArray());

            return dicErrors;
        }
    }
}
