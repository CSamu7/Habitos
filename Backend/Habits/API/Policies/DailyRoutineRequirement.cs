using Habits.Models;
using Microsoft.AspNetCore.Authorization;

namespace Habits.API.Policies
{
    public class DailyRoutineRequirement : IAuthorizationRequirement;

    public class DailyRoutineOwnerHandler : AuthorizationHandler<DailyRoutineRequirement, DailyTask>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DailyRoutineRequirement requirement, DailyTask resource)
        {
            string? owner = resource.IdRoutineNavigation.IdUserNavigation?.UserName;
            string? user = context.User.Identity?.Name;

            if (user == owner && user is not null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
