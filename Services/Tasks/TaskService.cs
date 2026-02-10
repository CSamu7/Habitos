using Habits.API.Tasks.DTO;
using Habits.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task = Habits.Models.Task;

namespace Habits.Services.Tasks
{
    public class TaskService
    {
        private HabitsContext _db;
        public TaskService(HabitsContext db) { 
            _db = db;
        }
        public async Task<Result<Task>> PostTask(int idUser, PostTaskRequest body)
        {
            Task task = body.ToTask();
            task.IdUser = idUser;

            if (task.IdGroup is not null)
            {
                Group? group = await _db.Groups.Where(group => group.IdGroup == task.IdGroup).SingleOrDefaultAsync();

                if (group is null) return Result<Task>.Failure(Status.InvalidData, $"Group with id {task.IdGroup} doesn't exist");
            }

            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();

            return Result<Habits.Models.Task>.Success(task);
        }
        public async Task<Result<Habits.Models.Task>> GetTask(int idTask)
        {
            Task? task = await _db.Tasks.FindAsync(idTask);

            if (task is null) return Result<Task>.Failure(Status.NotFound, "The task doesn't exist");

            return Result<Task>.Success(task);
        }
        public async Task<Result<List<Habits.Models.Task>>> GetAllTasks(int idUser)
        {
            List<Task>? tasks = await _db.Tasks
                .AsNoTracking()
                .Where(task => task.IdUser == idUser && task.IsActive)
                .ToListAsync();

            return Result<List<Habits.Models.Task>>.Success(tasks);
        }
        public async Task<Result<Habits.Models.Task>> DeleteTask(int idTask)
        {
            Task? task = await _db.Tasks.FindAsync(idTask);

            if (task is null) return Result<Task>.Failure(Status.NotFound, "The task doesn't exist");

            task.IsActive = false;
            await _db.SaveChangesAsync();

            return Result<Task>.Success(task);
        }
    }
}
