using Habits.API.DailyTasks;
using Habits.API.DailyTasks.DTO;
using Habits.Common;
using Habits.Features.DailyTasks.Models;
using Habits.Models;
using Habits.Services.DailyTasks;
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
    public Result<List<DailyTask>> GetDailyTasks(int idUser, GetAllFilters filters)
    {
        var dailyTasks = _db.DailyTasks
            .Include(d => d.IdTaskNavigation)
            .Where(d => d.IdTaskNavigation.IdUser == idUser)
            .AsNoTracking()
            .ToList();
        /*Esto sera removido porque primero checariamos si el usuario tiene permisos
         para este endpoint. Por ahora retornaremos NotFound si el usuario no existe.
         */
        if (dailyTasks.Count() == 0)
            return Result<List<DailyTask>>.Failure(Status.NotFound, "This user doesn't have daily tasks");

        List<DailyTask> filtered = Filter(dailyTasks, filters);

        return Result<List<DailyTask>>.Success(filtered);
    }
    private List<DailyTask> Filter(List<DailyTask> dailyTasks, GetAllFilters filters)
    {
        List<DailyTask> byDate = dailyTasks
            .Where(d =>
                DateTimeOffset.Compare(filters.DateStart.ToDateTimeOffset(), d.Date) <= 0 &&
                DateTimeOffset.Compare(d.Date, filters.DateEnd.ToDateTimeOffset()) <= 0)
            .ToList();

        if (filters.Progress is not null)
            byDate = byDate.Where(d => d.GetProgress() == filters.Progress).ToList();

        return byDate;
    }
    public async Task<Result<DailyTask>> PatchMinutes(int id, DailyTaskPatchRequest body)
    {
        DailyTask? dailyTask = await _db.DailyTasks.FindAsync(id);

        if (dailyTask is null)
            return Result<DailyTask>.Failure(Status.NotFound, "Daily dask doesn't exist");

        PatchValidation validation = new PatchValidation();

        Result<DailyTask> result = validation.Validate(dailyTask);
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
