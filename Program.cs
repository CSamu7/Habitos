using Habits.Features.Tasks;
using Habits.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

builder.Services.AddDbContext<HabitsContext>(options =>
{
    options.UseSqlServer(builder.Configuration["DB_Connection"]);
});

builder.Services.AddScoped<DailyTaskService>();

//Quiero entender que hacen especificamente estas lineas.

//ConfigureHttpJsonOptions. Configura las opciones para leer y escribir JSON
builder.Services.ConfigureHttpJsonOptions(options =>
{
    //Añade un convertidor, en este caso JsonSringEnumConverter.

    //JsonStringEnumConverter: Convierte valores enum a y de strings.
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
//

builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();

IEndpointRouteBuilder api = app.MapGroup("/api/");
IEndpointRouteBuilder users = api.MapGroup("/users/");
IEndpointRouteBuilder dailyTasks = api.MapGroup("/dailyTasks/");

users.AddUserRoute();
dailyTasks.AddDailyTaskRoute();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        opt.RoutePrefix = String.Empty;
    });
}

users.MapGet("/{idUser}/tasks", (int idUser, HabitsContext db) =>
{
    var tasks = db.Tasks.Where(task => task.IdUser == idUser);

    List<TaskGetDTO> tasksDTO = tasks.Select(task => task.Map()).ToList();

    return Results.Ok(tasksDTO);
});

app.UseExceptionHandler();

app.Run();
