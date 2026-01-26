using Habits.API;
using Habits.API.DailyTasks;
using Habits.Infraestructure;
using Habits.Models;
using Habits.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

app.UseStatusCodePages();
app.UseHabitsEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        opt.RoutePrefix = String.Empty;
    });
}
app.Run();
public partial class Program { }