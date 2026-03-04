using Habits.Models;
using Microsoft.AspNetCore.Authorization;

namespace Habits.API.Policies
{
    public class RoutineOwnerRequirement : IAuthorizationRequirement;
    public class RoutineAuthorizationHandler : AuthorizationHandler<RoutineOwnerRequirement, Routine>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            RoutineOwnerRequirement requirement, 
            Routine routine)
        {
            string? owner = routine.IdUserNavigation?.UserName;
            string? user = context.User.Identity?.Name;

            if (user == owner && user is not null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
