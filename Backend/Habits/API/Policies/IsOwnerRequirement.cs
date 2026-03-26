using Microsoft.AspNetCore.Authorization;

namespace Habits.API.Policies
{
    public class IsOwnerRequirement : IAuthorizationRequirement
    {
    }

    public class IsOwnerHandler : AuthorizationHandler<IsOwnerRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerRequirement requirement, string resource)
        {
            var username = context.User.Identity?.Name;

            if (username is not null && username == resource)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
