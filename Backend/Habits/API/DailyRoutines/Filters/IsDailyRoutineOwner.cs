using Habits.Models;
using Microsoft.AspNetCore.Authorization;

namespace Habits.API.DailyRoutines.Filters
{
    public class DailyRoutineOwnerRequirement : IAuthorizationRequirement;
    public class IsDailyRoutineOwner : AuthorizationHandler<DailyRoutineOwnerRequirement, DailyTask>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DailyRoutineOwnerRequirement requirement,
            DailyTask resource)
        {
            string? user = context.User.Identity?.Name;

            string? owner = resource.IdRoutineNavigation.IdUserNavigation.UserName;

            if (user == owner && user is not null)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
