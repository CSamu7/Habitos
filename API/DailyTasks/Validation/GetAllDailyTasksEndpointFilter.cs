using FluentValidation;
using Habits.API.DailyTasks.DTO;

namespace Habits.API.DailyTasks.Validation
{
    public class GetAllDailyTasksEndpointFilter : IEndpointFilter
    {
        IValidator<GetAllDailyTasksQueryParams> _validation;
        public GetAllDailyTasksEndpointFilter(IValidator<GetAllDailyTasksQueryParams> validation) { 
            _validation = validation;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            GetAllDailyTasksQueryParams filters = context.GetArgument<GetAllDailyTasksQueryParams>(1);

            var validationResult = _validation.Validate(filters);

            return validationResult.IsValid
                ? await next(context)
                : Results.ValidationProblem(validationResult.ToDictionary());
        }
    }
}
