using Habits.Features.DailyTasks;
using Habits.Features.DailyTasks.Models;
using Habits.Features.Tasks;
using Habits.Features.Tasks.Models;
using Habits.Features.Tasks.Validations;
using Habits.Models;
using Microsoft.EntityFrameworkCore;
public class DailyTaskService
{
    private HabitsContext _db;
    public DailyTaskService(HabitsContext db)
    {
        _db = db;
    }
    public async Task<DailyTask?> GetDailyTask(int idDailyTask)
    {
        var dailyTask = await _db.DailyTasks.FirstOrDefaultAsync(d => d.IdDailyTask == idDailyTask);

        return dailyTask;
    }
    //TODO: Si el filtro de progreso es nulo, entonces retornar todas las tareas.
    public Result<List<DailyTask>> GetDailyTasks(int idUser, GetAllDailyTaskFilters filters)
    {
        var dailyTasks = _db.DailyTasks
            .Where(d => d.IdTaskNavigation.IdUser == idUser);

        /*Esto sera removido porque primero checariamos si el usuario tiene permisos
         para este endpoint. Por ahora retornaremos NotFound si el usuario no existe.
         */
        if (dailyTasks.Count() == 0)
            return Result<List<DailyTask>>.Failure(Status.NotFound, "This user doesn't have daily tasks");

        var dailyTasksFiltered = dailyTasks
            .Include(d => d.IdTaskNavigation)
            .Where(d =>
                DateTimeOffset.Compare(filters.DateStart, d.Date) <= 0 &&
                DateTimeOffset.Compare(d.Date, filters.DateEnd) <= 0)
            .ToList();

        return filters.Progress is null
            ? Result<List<DailyTask>>.Success(dailyTasksFiltered)
            : Result<List<DailyTask>>.Success(dailyTasksFiltered
                .Where(d => d.GetProgress() == filters.Progress)
                .ToList()); 
    }
    public async Task<Result<DailyTask>> PatchMinutes(int id, DailyTaskPatchRequest body)
    {
        DailyTask? dailyTask = await _db.DailyTasks.FindAsync(id);

        if (dailyTask is null)
            return Result<DailyTask>.Failure(Status.NotFound, "Daily dask doesn't exist");

        DailyTaskValidation validation = new DailyTaskValidation(dailyTask);

        Result<DailyTask> result = validation.Validate();
        if (!result.Status.Equals(Status.Ok)) return result;

        IDailyTaskPatchCommand command = GetPatchCommand(body.Operation);

        command.ChangeMinutes(dailyTask, body);
        _db.SaveChanges();

        return Result<DailyTask>.Success(result.Value);
    }
    private IDailyTaskPatchCommand GetPatchCommand(PatchOperations operation)
    {
        return operation switch 
        { 
            PatchOperations.Add => new AddMinutes(), 
            PatchOperations.Replace => new OverwriteMinutes() 
        };
    }
}
