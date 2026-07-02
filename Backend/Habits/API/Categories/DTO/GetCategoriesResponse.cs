using Habits.Common;
using Habits.Models;

namespace Habits.API.Categories.DTO
{
    public record CategoryResponse(int Id, string Color, string Name);
    public record GetCategoriesResponse(List<CategoryResponse> List, int Count) : IBaseListResponse<CategoryResponse> { }

    public static class GetCategoriesResponseExtensions
    {
        public static GetCategoriesResponse ToGetCategoriesResponse(this List<RoutineCategory> categories)
        {
            var list = categories
                .Select(category => new CategoryResponse(category.IdRoutineCategory, category.Color, category.Name))
                .ToList();

            return new GetCategoriesResponse(list, list.Count);
        }

    }
}
