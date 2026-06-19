using FluentValidation;
using Habits.API.DailyRoutines.DTO;
using Habits.API.DailyRoutines.Filters;
using Habits.API.DailyRoutines.Validation;
using Habits.API.Policies;
using Habits.API.Routines.DTO;
using Habits.API.Routines.Validation;
using Habits.API.Users.DTO;
using Habits.API.Users.Validations;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

namespace Habits.API
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
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

            services.AddScoped(filters => new GetDailyRoutineQueryParams(today.AddDays(-1), today, []));

            return services;
        }

        public static IServiceCollection AddValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<GetDailyRoutineQueryParams>, DailyRoutineQueryParamsValidation>();
            services.AddScoped<IValidator<PatchDailyRoutineRequest>, PatchDailyTaskValidation>();
            services.AddScoped<IValidator<PostRoutineRequest>, PostRoutineRequestValidation>();
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidation>();
            services.AddScoped<IValidator<PatchUserRequest>, PatchUserRequestValidation>();
            services.AddScoped<IAuthorizationHandler, RoutineAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, IsOwnerHandler>();
            services.AddScoped<IAuthorizationHandler, IsDailyRoutineOwner>();

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
