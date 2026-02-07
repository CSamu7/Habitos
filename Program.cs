using FluentValidation;
using Habits.API;
using Habits.API.DailyTasks;
using Habits.API.DailyTasks.DTO;
using Habits.API.DailyTasks.Validation;
using Habits.API.Tasks;
using Habits.API.Users;
using Habits.Infraestructure;
using Habits.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IValidator<GetAllDailyTasksQueryParams>, GetAllFiltersValidation>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services
    .AddSwagger()
    .AddApiServices()
    .AddDatabase(builder.Configuration)
    .AddDailyTasks();

var app = builder.Build();

app.UseExceptionHandler();
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

baseApi.MapDailyTasks();
baseApi.MapUsers();
baseApi.MapTasks();

app.Run();
public partial class Program { }