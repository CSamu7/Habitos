using Habits.API.Policies;
using Habits.Services.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Habits.API.Routines.Filters
{
    public class IsSameRoutineOwnerFilter : IEndpointFilter
    {
        private readonly IAuthorizationService _authService;
        private readonly RoutineService _service;
        public IsSameRoutineOwnerFilter(RoutineService service, IAuthorizationService authService)
        {
            _service = service;
            _authService = authService;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var user = context.HttpContext.User;
            var idRoutine = context.GetArgument<int>(0);

            var result = await _service.GetRoutine(idRoutine);

            if (result.Status.Equals(Status.NotFound))
                return TypedResults.NotFound();

            var authResult = await _authService.AuthorizeAsync
                (user, result.Value, new RoutineOwnerRequirement());

            if (!authResult.Succeeded)
                return TypedResults.Forbid();

            return await next(context);
        }
    }
}
