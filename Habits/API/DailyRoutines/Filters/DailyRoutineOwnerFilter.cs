using Habits.API.Policies;
using Habits.Models;
using Microsoft.AspNetCore.Authorization;

namespace Habits.API.DailyRoutines.Filters
{
    public class DailyRoutineOwnerFilter : IEndpointFilter
    {
        private readonly IAuthorizationService _authService;
        private readonly DailyRoutineService _service;
        public DailyRoutineOwnerFilter(IAuthorizationService authService, DailyRoutineService service) { 
            _authService = authService;
            _service = service;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
                var user = context.HttpContext.User;
                var idRoutine = context.GetArgument<int>(0);

                var result = await _service.GetRoutine(idRoutine);

                if (result.Status.Equals(Status.NotFound))
                    return TypedResults.NotFound();

                var authResult = await _authService.AuthorizeAsync
                    (user, result.Value, new DailyRoutineOwnerRequirement());

                if (!authResult.Succeeded)
                    return TypedResults.Forbid();

                return await next(context);
        }
    }
}
