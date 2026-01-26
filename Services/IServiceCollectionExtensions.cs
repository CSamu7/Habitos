namespace Habits.Services
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDailyTasks(this IServiceCollection services)
        {
            services.AddScoped<DailyTaskService>();

            return services;
        }
    }
}
