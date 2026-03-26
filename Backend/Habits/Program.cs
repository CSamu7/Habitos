using FluentValidation;
using Habits.API;
using Habits.API.DailyRoutines;
using Habits.API.DailyRoutines.DTO;
using Habits.API.DailyRoutines.Filters;
using Habits.API.DailyRoutines.Validation;
using Habits.API.Policies;
using Habits.API.Routines;
using Habits.API.Routines.DTO;
using Habits.API.Routines.Validation;
using Habits.API.Users;
using Habits.API.Users.DTO;
using Habits.API.Users.Validations;
using Habits.Infraestructure;
using Habits.Models;
using Habits.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddScoped<IValidator<GetDailyRoutineQueryParams>, DailyRoutineQueryParamsValidation>();
builder.Services.AddScoped<IValidator<PatchDailyRoutineRequest>, PatchDailyTaskValidation>();
builder.Services.AddScoped<IValidator<PostRoutineRequest>, PostRoutineRequestValidation>();
builder.Services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidation>();
builder.Services.AddScoped<IAuthorizationHandler, RoutineAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, IsOwnerHandler>();
builder.Services.AddScoped<IAuthorizationHandler, IsDailyRoutineOwner>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<HabitsContext>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

//builder.Services.AddAuthentication(options =>
//{
//    options.RequireAuthenticatedSignIn = true;
//});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("IsRoutineOwner", policy => policy.Requirements.Add(new RoutineOwnerRequirement()));


builder.Services
    .AddSwagger()
    .AddApiServices()
    .AddDatabase(builder.Configuration)
    .AddDbServices();

var app = builder.Build();

app.UseStatusCodePages();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        opt.RoutePrefix = String.Empty;
    });
}

var baseApi = app.MapGroup("/api/");

baseApi.MapDailyRoutines();
baseApi.MapUsers();
baseApi.MapRoutines();

app.Run();
public partial class Program { }
