var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IEndpointRouteBuilder api = app.MapGroup("/api");

IEndpointRouteBuilder users = api.MapGroup("/users");

app.Run();
