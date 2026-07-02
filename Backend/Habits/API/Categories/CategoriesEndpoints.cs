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

            if (user is null) return TypedResults.Unauthorized();

            Result<List<RoutineCategory>> categories = await service.GetAll(user.Id);

            if (categories.Status != Status.Ok) return categories.ToHttpResponse();

            GetCategoriesResponse response = categories.Value.ToGetCategoriesResponse();

            return TypedResults.Ok(response);
        }
    }
}
