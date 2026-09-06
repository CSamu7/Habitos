using Habits.API.Categories.DTO;
using Habits.Models;
using Habits.Services.Categories;
using Microsoft.AspNetCore.Identity;

namespace Habits.API.Categories
{
    public class CategoriesEndpoints
    {
        public async static Task<IResult> GetAll(string username, UserManager<User> manager, CategoryService service, HttpContext ctx)
        {
            User? user = await manager.GetUserAsync(ctx.User);

            if (user is null || user.UserName != username) return TypedResults.Unauthorized();

            Result<List<Category>> categories = await service.GetAll(user.Id);

            if (categories.Status != Status.Ok) return categories.ToHttpResponse();

            GetCategoriesResponse response = categories.Value.ToGetCategoriesResponse();

            return TypedResults.Ok(response);
        }

        public async static Task<IResult> Post(string username, PostCategoryRequest body, LinkGenerator linkGenerator, UserManager<User> manager, CategoryService service, HttpContext ctx)
        {
            User? user = await manager.GetUserAsync(ctx.User);

            if (user is null || user.UserName != username) return TypedResults.Unauthorized();

            Result<Category> result = await service.Post(user, body);

            if (result.Status != Status.Ok) return result.ToHttpResponse();

            string? uri = linkGenerator.GetPathByName
                ("getCategory", new() { { "idCategory", result.Value.IdCategory } });

            return TypedResults.Created(uri,result.Value.ToGetCategoryResponse());
        }

        public async static Task<IResult> Get()
        {
            return TypedResults.Ok();
        }
    }
}
