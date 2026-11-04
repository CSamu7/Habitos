using Habits.API.Routines.DTO;
using Habits.Common;
using Habits.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Habits.Services.Tasks
{
    public class RoutineService
    {
        private readonly HabitsContext _db;
        public RoutineService(HabitsContext db)
        {
            _db = db;
        }
        public async Task<Result<Routine>> GetRoutine(int idRoutine)
        {
            Routine? task = await _db.Routines
                .AsNoTracking()
                .Include(x => x.IdUserNavigation)
                .Include(x => x.Categories)
                .SingleOrDefaultAsync(x => x.IdRoutine == idRoutine);

            if (task is null) return Result<Routine>.Failure(Status.NotFound, "No se ha encontrado la rutina");

            return Result<Routine>.Success(task);
        }
        public async Task<Result<List<Routine>>> GetAllRoutines(string idUser)
        {
            List<Routine>? routines = await _db.Routines
                .AsNoTracking()
                .Include(x => x.Categories)
                .Where(routine => routine.IdUser == idUser)
                .ToListAsync();

            return Result<List<Routine>>.Success(routines);
        }
        public async Task<Result<Routine>> PostRoutine(string idUser, PostRoutineRequest body)
        {
            var user = await _db.Users.Include(u => u.Routines).Include(u => u.RoutineCategories).FirstOrDefaultAsync(user => user.Id == idUser);

            if (user.GetTotalMinutes() + body.Minutes > Constants.MAX_FULL_ROUTINE_MINUTES)
                return Result<Routine>.Failure(Status.InvalidData, "No puedes superar el limite de 8 horas");

            List<Category> categories = [];

            foreach (int idCategory in body.Categories)
            {
                Category? categoryDb = await _db.Categories
                        .FirstOrDefaultAsync(c => c.IdCategory == idCategory);

                if (categoryDb is not null) categories.Add(categoryDb);
            }

            Routine routine = new Routine { IdUser = idUser, Categories = categories, IsActive = true, Minutes = body.Minutes, Name = body.Name};

            await _db.Routines.AddAsync(routine);
            await _db.SaveChangesAsync();

            return Result<Routine>.Success(routine);
        }

        public async Task<Result<Routine>> PatchTask(int idRoutine, JsonPatchDocument<PatchRoutineRequest> body)
        {
            var routine = await _db.Routines
                .Include(x => x.IdUserNavigation)
                .Include(x => x.Categories)
                .SingleOrDefaultAsync(x => x.IdRoutine == idRoutine);

            if (routine is null) return Result<Routine>.Failure(Status.NotFound, "No encontrado");

            PatchRoutineRequest cloneRoutine = new(routine.Name, routine.Minutes, routine.Categories.Select(c => c.IdCategory).ToList());
            body.ApplyTo(cloneRoutine);

            foreach(var idCategory in cloneRoutine.Categories)
            {
                Category? category = await _db.Categories.FirstOrDefaultAsync(c => c.IdCategory == idCategory);

                if (category is not null) routine.AppendCategory(category);
            }

            routine.ChangeMinutes(cloneRoutine.Minutes);
            routine.ChangeName(cloneRoutine.Name);

            await _db.SaveChangesAsync();
            return Result<Routine>.Success(routine);
        }
        public async Task<Result<Routine>> DeleteTask(int idRoutine)
        {
            Routine? task = await _db.Routines
                .Include(x => x.IdUserNavigation)
                .Include(x => x.Categories)
                .SingleOrDefaultAsync(x => x.IdRoutine == idRoutine);

            if (task is null) return Result<Routine>.Failure(Status.NotFound, "No se ha encontrado la rutina");

            task.Deactivate();
            await _db.SaveChangesAsync();

            return Result<Routine>.Success(task);
        }
    }
}
