using Habits.Models;
using Microsoft.EntityFrameworkCore;

namespace Habits.Services.Categories
{
    public class CategoryService
    {
        private HabitsContext _ctx;
        public CategoryService(HabitsContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Result<List<RoutineCategory>>> GetAll(string idUser)
        {
            var categories = await _ctx.RoutineCategories.Where(category => category.IdUser == idUser).ToListAsync();

            return Result<List<RoutineCategory>>.Success(categories);
        }
    }
}
