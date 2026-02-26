
using FluentValidation;
using Habits.API.Routines.DTO;

namespace Habits.API.Routines.Filters
{
    public class PostRoutineFilter : IEndpointFilter
    {
        private readonly IValidator<PostRoutineRequest> _validation;
        public PostRoutineFilter(IValidator<PostRoutineRequest> validation)
        {
            _validation = validation;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var body = context.GetArgument<PostRoutineRequest>(1);

            var result = _validation.Validate(body);

            if (!result.IsValid)
                return TypedResults.ValidationProblem(result.ToDictionary());

            return await next(context);
        }
    }
}
