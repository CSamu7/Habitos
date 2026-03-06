
using Microsoft.AspNetCore.Authorization;

namespace Habits.API.Policies
{
    
    public class IsOwnerFilter : IEndpointFilter
    {
        private readonly IAuthorizationService _authService;
        public IsOwnerFilter(IAuthorizationService handler)
        {
            _authService = handler;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var user = context.HttpContext.User;
            var userFromQuery = context.GetArgument<string>(0);

            var result = await _authService.AuthorizeAsync(user, userFromQuery, new IsOwnerRequirement());

            return result.Succeeded
                ? await next(context)
                : TypedResults.Forbid();
        }
    }
}
