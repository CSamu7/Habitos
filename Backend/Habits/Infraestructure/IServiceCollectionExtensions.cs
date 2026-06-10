using Habits.API;
using Habits.Models;
using Microsoft.AspNetCore.Identity;
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

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<HabitsContext>()
.AddErrorDescriber<IdentitySpanishDescriber>();

            return services;
        }
    }
}
