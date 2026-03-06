using Habits.API.DailyRoutines;
using Habits.API.DailyRoutines.Validation;
using Habits.API.Policies;
using Habits.API.Routines;
using Habits.API.Routines.DTO;
using Habits.API.Routines.Filters;
using Habits.API.Users.Filters;

namespace Habits.API.Users
{
    public static class UserRoutes
    {
        public static IEndpointRouteBuilder MapUsers(this IEndpointRouteBuilder router)
        {
            var userRoutes = router.MapGroup("/users");

            //Nested dailyTasks
            userRoutes.MapGet("/{username}/dailyRoutines/today", DailyRoutineEndpoints.GetDailyRoutines)
                .AddEndpointFilter<IsOwnerFilter>();

            userRoutes.MapGet("/{username}/dailyRoutines", DailyRoutineEndpoints.GetDailyRoutines)
                .AddEndpointFilter<IsOwnerFilter>()
                .AddEndpointFilter<GetAllDailyRoutinesEndpointFilter>();

            //Nestad Tasks
            userRoutes.MapPost("/{username}/routines", RoutineEndpoints.PostRoutine)
                .AddEndpointFilter<IsOwnerFilter>()
                .AddEndpointFilter<PostRoutineFilter>();

            userRoutes.MapGet("/{username}/routines", RoutineEndpoints.GetAllRoutines)
                .AddEndpointFilter<IsOwnerFilter>()
                .WithName("getAllTasks")
                .Produces<List<GetRoutineResponse>>(200);

            //User Routes
            userRoutes.MapGet("", UserEndpoints.GetUser)
                .WithName("getUser");
            
            userRoutes.MapPost("/register", UserEndpoints.Register)
                .AddEndpointFilter<RegisterUserEndpointFilter>()
                .WithName("registerUser");

            userRoutes.MapPost("/login", UserEndpoints.Login)
                .WithName("loginUser");

            return router;
        }
    }
}
