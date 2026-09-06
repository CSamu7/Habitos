using Habits.API.Users.Filters;

namespace Habits.API.Users
{
    public static class UserRoutes
    {
        public static IEndpointRouteBuilder MapUsers(this IEndpointRouteBuilder router)
        {
            var userRoutes = router.MapGroup("/users");

            userRoutes.MapGet("", UserEndpoints.GetUser)
                .WithName("getUser");

            userRoutes.MapPost("/register", UserEndpoints.Register)
                .AddEndpointFilter<RegisterUserEndpointFilter>()
                .WithName("registerUser");

            userRoutes.MapPatch("", UserEndpoints.ModifyUser)
                .WithName("modifyUser").Produces(200);

            userRoutes.MapPost("/login", UserEndpoints.Login)
                .WithName("loginUser");

            return router;
        }
    }
}
