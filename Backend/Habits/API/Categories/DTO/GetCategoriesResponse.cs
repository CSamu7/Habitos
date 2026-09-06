using Habits.Common;
using Habits.Models;

namespace Habits.API.Categories.DTO
{
    public record MinimalCategory(int IdCategory, string Name, string Color = "FFF");
    public record GetCategoriesResponse(List<MinimalCategory> List, int Count) : IBaseListResponse<MinimalCategory> { }

    public static class GetCategoriesResponseExtensions
    {
        public static MinimalCategory ToGetCategoryResponse(this Category category) =>
             new MinimalCategory(category.IdCategory, category.Name, category.Color);
        public static GetCategoriesResponse ToGetCategoriesResponse(this List<Category> categories)
        {
            var list = categories
                .Select(category => category.ToGetCategoryResponse())
                .ToList();

            return new GetCategoriesResponse(list, list.Count);
        }

    }
}
