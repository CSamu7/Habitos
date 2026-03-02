using Habits.API.DailyRoutines;
using Habits.API.DailyRoutines.Validation;
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
            userRoutes.MapGet("/{idUser}/dailyRoutines/today", DailyRoutineEndpoints.GetDailyRoutines);
            userRoutes.MapGet("/{idUser}/dailyRoutines", DailyRoutineEndpoints.GetDailyRoutines)
            .AddEndpointFilter<GetAllDailyRoutinesEndpointFilter>();

            //Nestad Tasks
            userRoutes.MapPost("/{idUser}/routines", RoutineEndpoints.PostRoutine)
                .AddEndpointFilter<PostRoutineFilter>();

            userRoutes.MapGet("/{idUser}/routines", RoutineEndpoints.GetAllRoutines)
                .WithName("getAllTasks")
                .Produces<List<GetRoutineResponse>>(200);

            //User Routes
            userRoutes.MapGet("/{idUser}", UserEndpoints.GetUser);
            userRoutes.MapPost("/register", UserEndpoints.Register)
                .AddEndpointFilter<RegisterUserEndpointFilter>()
                .WithName("registerUser");
            userRoutes.MapGet("/login", UserEndpoints.Login);

            return router;
        }
    }
}
