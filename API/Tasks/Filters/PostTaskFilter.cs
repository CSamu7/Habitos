
using FluentValidation;
using Habits.API.Tasks.DTO;

namespace Habits.API.Tasks.Filters
{
    public class PostTaskFilter : IEndpointFilter
    {
        private readonly IValidator<PostTaskRequest> _validation;
        public PostTaskFilter(IValidator<PostTaskRequest> validation)
        {
            _validation = validation;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var body = context.GetArgument<PostTaskRequest>(1);

            var result = _validation.Validate(body);

            if (!result.IsValid)
                return TypedResults.ValidationProblem(result.ToDictionary());

            return await next(context);
        }
    }
}
