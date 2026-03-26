using Habits.API.Routines.DTO;
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
                .Include(x => x.IdUserNavigation)
                .SingleOrDefaultAsync(x => x.IdRoutine == idRoutine);

            if (task is null) return Result<Routine>.Failure(Status.NotFound, "The routine doesn't exist");

            return Result<Routine>.Success(task);
        }
        public async Task<Result<List<Routine>>> GetAllRoutines(string idUser)
        {
            List<Routine>? tasks = await _db.Routines
                .AsNoTracking()
                .Where(task => task.IdUser == idUser && task.IsActive)
                .ToListAsync();

            return Result<List<Routine>>.Success(tasks);
        }
        public async Task<Result<Routine>> PostRoutine(string id, PostRoutineRequest body)
        {
            Routine routine = body.ToTask();
            routine.IdUser = id;

            if (routine.IdCategory is not null)
            {
                Category? group = await _db.Categories.Where(group => group.IdCategory == routine.IdCategory).SingleOrDefaultAsync();

                if (group is null) return Result<Routine>.Failure(Status.InvalidData, $"Category #{routine.IdCategory} doesn't exist");
            }

            _db.Routines.Add(routine);
            await _db.SaveChangesAsync();

            return Result<Routine>.Success(routine);
        }
        //TODO: Implementar pruebas de integración.
        public async Task<Result<Routine>> PatchTask(int idRoutine, JsonPatchDocument body)
        {
            var result = await GetRoutine(idRoutine);

            PostRoutineRequest patchRoutine = result.Value.FromTask();
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
