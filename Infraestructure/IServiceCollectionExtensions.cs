using Habits.Models;
using Microsoft.EntityFrameworkCore;

namespace Habits.Infraestructure
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<HabitsContext>(options =>
            {
                options.UseSqlServer(config["DB_Connection"]);
            });

            return services;
        }
    }
}
