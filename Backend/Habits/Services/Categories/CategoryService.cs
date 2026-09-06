using Habits.API.Categories.DTO;
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
        public async Task<Result<List<Category>>> GetAll(string idUser)
        {
            var categories = await _ctx.Categories.Where(category => category.IdUser == idUser).ToListAsync();

            return Result<List<Category>>.Success(categories);
        }

        public async Task<Result<Category>> Post(User user, PostCategoryRequest body)
        {
            List<Routine> routines = [];

            if(body.Routines.Length > 0)
            {
                routines = body.Routines
                    .Select(async idRoutine => await _ctx.Routines.FirstOrDefaultAsync(dbRoutine => dbRoutine.IdRoutine == idRoutine))
                    .Select(routine => routine.Result)
                    .OfType<Routine>().ToList() ?? [];
            }

            Category category = new Category { Name = body.Name, Color = body.Color, IdUser = user.Id, Routines = routines };
            await _ctx.AddAsync(category);
            await _ctx.SaveChangesAsync();

            return Result<Category>.Success(category);
        }
    }
}
