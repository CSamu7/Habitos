using Habits.API.Categories.DTO;

namespace Habits.API.Categories
{
    public static class CategoriesRoutes
    {
        public static IEndpointRouteBuilder MapCategories(this IEndpointRouteBuilder builder)
        {
            var usernameGroup = builder.MapGroup("users/{username}");
            var categoriesGroup = builder.MapGroup("/categories");

            usernameGroup.MapGet("/categories", CategoriesEndpoints.GetAll)
                .WithName("getAllCategories")
                .Produces<GetCategoriesResponse>()
                .ProducesProblem(404);

            usernameGroup.MapPost("/categories", CategoriesEndpoints.Post)
                .WithName("postCategory");

            categoriesGroup.MapGet("/{idCategory}", CategoriesEndpoints.Get)
                .WithName("getCategory");

            return builder;
        }
    }
}
