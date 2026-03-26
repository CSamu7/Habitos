
using FluentValidation;
using Habits.API.Users.DTO;

namespace Habits.API.Users.Filters
{
    public class RegisterUserEndpointFilter : IEndpointFilter
    {
        private readonly IValidator<RegisterUserRequest> _validation;
        public RegisterUserEndpointFilter(IValidator<RegisterUserRequest> validation)
        {
            _validation = validation;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            RegisterUserRequest dto = context.GetArgument<RegisterUserRequest>(0);

            var result = await _validation.ValidateAsync(dto);

            if (!result.IsValid)
                return TypedResults.ValidationProblem(result.ToDictionary());

            return await next(context);
        }
    }
}
