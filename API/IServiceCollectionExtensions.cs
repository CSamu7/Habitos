using Habits.API.DailyTasks.DTO;
using System.Text.Json.Serialization;

namespace Habits.API
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //Quiero entender que hacen especificamente estas lineas.

            //ConfigureHttpJsonOptions. Configura las opciones para leer y escribir JSON
            services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddProblemDetails();
            services.AddSingleton(TimeProvider.System);

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            services.AddScoped<GetAllDailyTasksQueryParams>
                (filters => new GetAllDailyTasksQueryParams(today.AddDays(-1), today, null));

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
