using FluentValidation;
using Habits.API.DailyRoutines.DTO;

namespace Habits.API.DailyRoutines.Validation
{
    public class GetAllDailyRoutinesEndpointFilter : IEndpointFilter
    {
        IValidator<GetDailyRoutineQueryParams> _validation;
        public GetAllDailyRoutinesEndpointFilter(IValidator<GetDailyRoutineQueryParams> validation)
        {
            _validation = validation;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            GetDailyRoutineQueryParams filters = context.GetArgument<GetDailyRoutineQueryParams>(1);

            var validationResult = _validation.Validate(filters);

            return validationResult.IsValid
                ? await next(context)
                : Results.ValidationProblem(validationResult.ToDictionary());
        }
    }
}
