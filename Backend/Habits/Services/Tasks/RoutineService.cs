using Habits.API.Routines.DTO;
using Habits.Common;
using Habits.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Habits.Services.Tasks
{
    public class RoutineService
    {
        private HabitsContext _db;
        public RoutineService(HabitsContext db, UserManager<User> authService)
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
                .Where(routine => routine.IdUser == idUser && routine.IsActive)
                .ToListAsync();

            return Result<List<Routine>>.Success(routines);
        }
        public async Task<Result<Routine>> PostRoutine(string idUser, PostRoutineRequest body)
        {
            int totalMinutes = _db.Routines.Where(r => r.IdUser == idUser).Sum(routine => routine.Minutes);

            if (totalMinutes > Constants.MAX_FULL_ROUTINE_MINUTES)
                return Result<Routine>.Failure(Status.InvalidData, "No puedes superar el limite de 8 horas");

            Routine routine = new Routine { IdUser = idUser, Name = body.Name, Minutes = body.Minutes, IsActive = true};
            
            //TODO: transform to linq
            foreach (int idCategory in body.Categories)
            {
                Category? categoryDb = await _db.Categories
                        .FirstOrDefaultAsync(c => c.IdCategory == idCategory);

                if (categoryDb == null) continue;

                routine.Categories.Add(new Category { Name = categoryDb.Name, Color = categoryDb.Color, IdUser = idUser});
            }

            await _db.Routines.AddAsync(routine);
            await _db.SaveChangesAsync();

            return Result<Routine>.Success(routine);
        }
        //TODO: Checar si tenemos pruebas de integracion para esto.
        public async Task<Result<Routine>> PatchTask(int idRoutine, JsonPatchDocument body)
        {
            var result = await GetRoutine(idRoutine);
            Routine routine = result.Value;
            List<int> categories = routine.Categories.Select(c => c.IdCategory).ToList();

            PostRoutineRequest patchRoutine = new PostRoutineRequest(routine.Name, routine.Minutes, categories);
            body.ApplyTo(patchRoutine);

            await _db.SaveChangesAsync();

            return Result<Routine>.Success(result.Value);
        }
        public async Task<Result<Routine>> DeleteTask(int idRoutine)
        {
            var result = await GetRoutine(idRoutine);

            result.Value.IsActive = false;
            await _db.SaveChangesAsync();

            return Result<Routine>.Success(result.Value);
        }
    }
}
