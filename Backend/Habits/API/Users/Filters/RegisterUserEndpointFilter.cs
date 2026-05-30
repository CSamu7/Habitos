
using FluentValidation;
using Habits.API.Users.DTO;
using Habits.API.Users.Validations;
using Habits.Models;

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

            var validation = _validation.Validate(dto);

            if (!validation.IsValid)
                return TypedResults.ValidationProblem(validation.ToDictionary());

            return await next(context);
        }
    }
}
