using Habits.API;
using Habits.API.Categories;
using Habits.API.DailyRoutines;
using Habits.API.Policies;
using Habits.API.Routines;
using Habits.API.Users;
using Habits.Infraestructure;
using Habits.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("IsRoutineOwner", policy => policy.Requirements.Add(new RoutineOwnerRequirement()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("PagesAllowed", policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

builder.Services
    .AddSwagger()
    .AddValidations()
    .AddApiServices()
    .AddIdentity()
    .AddDatabase(builder.Configuration)
    .AddDbServices();

var app = builder.Build();

app.UseCors("PagesAllowed");
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorapp =>
    {
        errorapp.Run(async context =>
        {

            var a = TypedResults.Problem("Hubo un problema en el servidor", title: "Problema en el servidor");
            await context.Response.WriteAsJsonAsync(a.ProblemDetails);
        });
    });
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        opt.RoutePrefix = String.Empty;
    });
}

var baseApi = app.MapGroup("/api");

baseApi.MapDailyRoutines()
    .MapUsers()
    .MapRoutines()
    .MapCategories();

app.Run();
public partial class Program { }
