using FluentValidation;
using Habits.API.DailyTasks.DTO;

namespace Habits.API.DailyTasks.Validation
{
    public class GetAllDailyTasksEndpointFilter : IEndpointFilter
    {
        IValidator<GetDailyTasksQueryParams> _validation;
        public GetAllDailyTasksEndpointFilter(IValidator<GetDailyTasksQueryParams> validation) { 
            _validation = validation;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            GetDailyTasksQueryParams filters = context.GetArgument<GetDailyTasksQueryParams>(1);

            var validationResult = _validation.Validate(filters);

            return validationResult.IsValid
                ? await next(context)
                : Results.ValidationProblem(validationResult.ToDictionary());
        }
    }
}
