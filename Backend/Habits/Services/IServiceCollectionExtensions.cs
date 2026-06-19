using Habits.Services.Tasks;
using Habits.Services.Users;

namespace Habits.Services
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            services.AddScoped<DailyRoutineService>();
            services.AddScoped<RoutineService>();
            services.AddScoped<UserService>();

            return services;
        }
    }
}
