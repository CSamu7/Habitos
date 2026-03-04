using Habits.API.Policies;
using Habits.API.Routines.DTO;
using Habits.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Habits.Services.Tasks
{
    public class RoutineService
    {
        private HabitsContext _db;
        private IAuthorizationService _authService;
        public RoutineService(HabitsContext db, IAuthorizationService authService)
        {
            _db = db;
            _authService = authService;
        }
        public async Task<Result<Routine>> GetRoutine(int idRoutine)
        {
            Routine? task = await _db.Routines.FindAsync(idRoutine);

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
        public async Task<Result<Routine>> PostRoutine(string idUser, PostRoutineRequest body)
        {
            Routine routine = body.ToTask();
            routine.IdUser = idUser;

            if (routine.IdCategory is not null)
            {
                Category? group = await _db.Categories.Where(group => group.IdCategory == routine.IdCategory).SingleOrDefaultAsync();

                if (group is null) return Result<Routine>.Failure(Status.InvalidData, $"Category #{routine.IdCategory} doesn't exist");
            }

            await _db.Routines.AddAsync(routine);
            await _db.SaveChangesAsync();

            return Result<Habits.Models.Routine>.Success(routine);
        }
        public async Task<Result<Routine>> PatchTask(int idTask, JsonPatchDocument body)
        {
            Routine? task = await _db.Routines.FindAsync(idTask);

            if (task is null) return Result<Routine>.Failure(Status.NotFound, "The task doesn't exist");

            PostRoutineRequest test = task.FromTask();
            body.ApplyTo(test);

            await _db.SaveChangesAsync();

            return Result<Routine>.Success(task);
        }
        public async Task<Result<Routine>> DeleteTask(int idTask)
        {
            Routine? task = await _db.Routines.FindAsync(idTask);

            if (task is null) return Result<Routine>.Failure(Status.NotFound, "The task doesn't exist");

            task.IsActive = false;
            await _db.SaveChangesAsync();

            return Result<Routine>.Success(task);
        }
    }
}
