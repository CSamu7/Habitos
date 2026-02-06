using FluentValidation;

namespace Habits.API.DailyTasks.Validation
{
    public class GetAllEndpointFilter : IEndpointFilter
    {
        IValidator<GetAllFilters> _validation;
        public GetAllEndpointFilter(IValidator<GetAllFilters> validation) { 
            _validation = validation;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            GetAllFilters filters = context.GetArgument<GetAllFilters>(1);

            var validationResult = _validation.Validate(filters);

            return validationResult.IsValid
                ? await next(context)
                : Results.ValidationProblem(validationResult.ToDictionary());
        }
    }
}
