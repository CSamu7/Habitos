using Habits.API.DailyRoutines.DTO;
using Habits.API.DailyRoutines.Validation;

namespace Habits.Features.DailyTasks.Filters
{
    public class DailyRoutinePatchFilter : IEndpointFilter
    {
        //TODO: Move to API/DailyRoutines
        private PatchDailyTaskValidation _validation;
        public DailyRoutinePatchFilter(PatchDailyTaskValidation validation)
        {
            _validation = validation;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var body = context.GetArgument<PatchDailyRoutineRequest>(1);

            var result = await _validation.ValidateAsync(body);

            if (!result.IsValid)
                return TypedResults.ValidationProblem(
                    result.ToDictionary()
                );

            return await next(context);
        }
    }
}
