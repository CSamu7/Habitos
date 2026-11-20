using Habits.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Habits.API.Policies
{
    public class RoutineOwnerRequirement : IAuthorizationRequirement;
    public class RoutineAuthorizationHandler : AuthorizationHandler<RoutineOwnerRequirement, Routine>
    {
        private UserManager<User> _userManager;
        public RoutineAuthorizationHandler(UserManager<User> userManager) { 
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RoutineOwnerRequirement requirement,
            Routine routine)
        {
            string? userFromRoutine = routine.IdUser;
            string? userFromAuth = _userManager.GetUserId(context.User);

            if (userFromAuth == userFromRoutine && userFromAuth is not null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
