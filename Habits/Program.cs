using FluentValidation;
using Habits.API;
using Habits.API.DailyRoutines;
using Habits.API.DailyRoutines.DTO;
using Habits.API.DailyRoutines.Validation;
using Habits.API.Routines;
using Habits.API.Routines.DTO;
using Habits.API.Routines.Validation;
using Habits.API.Users;
using Habits.Infraestructure;
using Habits.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddScoped<IValidator<GetDailyRoutineQueryParams>, DailyRoutineQueryParamsValidation>();
builder.Services.AddScoped<IValidator<PatchDailyRoutineRequest>, PatchDailyTaskValidation>();
builder.Services.AddScoped<IValidator<PostRoutineRequest>, PostRoutineRequestValidation>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services
    .AddSwagger()
    .AddApiServices()
    .AddDatabase(builder.Configuration)
    .AddDbServices();

var app = builder.Build();

app.UseStatusCodePages();

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
