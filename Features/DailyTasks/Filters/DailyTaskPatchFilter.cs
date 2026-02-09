using Habits.API.DailyTasks.DTO;
using Habits.API.DailyTasks.Validation;

namespace Habits.Features.DailyTasks.Filters
{
    public class DailyTaskPatchFilter : IEndpointFilter
    {
        private PatchDailyTaskValidation _validation;
        public DailyTaskPatchFilter(PatchDailyTaskValidation validation)
        {
            _validation = validation;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var body = context.GetArgument<PatchDailyTaskRequest>(1);

            var result = await _validation.ValidateAsync(body);

            if (!result.IsValid)
                return TypedResults.ValidationProblem(
                    result.ToDictionary()
                );

            return await next(context);
        }
    }
}
