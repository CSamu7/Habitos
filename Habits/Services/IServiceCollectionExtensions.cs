using Habits.Services.Tasks;

namespace Habits.Services
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            services.AddScoped<DailyRoutineService>();
            services.AddScoped<RoutineService>();

            return services;
        }
    }
}
