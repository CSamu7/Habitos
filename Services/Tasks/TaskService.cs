using Habits.API.Tasks.DTO;
using Habits.Models;
using Task = Habits.Models.Task;

namespace Habits.Services.Tasks
{
    public class TaskService
    {
        private HabitsContext _db;
        public TaskService(HabitsContext db) { 
            _db = db;
        }
        public async Task<Result<int>> PostTask(int idUser, PostTaskBody body)
        {
            Task task = body.ToTask(idUser);

            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();

            return Result<int>.Success(task.IdTask);
        }
    }
}
