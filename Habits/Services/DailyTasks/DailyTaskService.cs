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
    public async Task<Result<DailyTask>> GetDailyTask(int idDailyTask)
    {
        var dailyTask = await _db.DailyTasks
            .Include(d => d.IdTaskNavigation)
            .FirstOrDefaultAsync(d => d.IdDailyTask == idDailyTask);

        if (dailyTask is null) return Result<DailyTask>.Failure(Status.NotFound, "Daily task doesn't exist");

        return Result<DailyTask>.Success(dailyTask);
    }
    public Result<List<DailyTask>> GetDailyTasks(int idUser, GetDailyTasksQueryParams queryParams)
    {
        var dailyTasks = _db.DailyTasks
            .Include(d => d.IdTaskNavigation)
            .Where(d => d.IdTaskNavigation.IdUser == idUser)
            .AsNoTracking()
            .ToList();

        List<DailyTask> filtered = Filter(dailyTasks, queryParams);

        return Result<List<DailyTask>>.Success(filtered);
    }
    private List<DailyTask> Filter(List<DailyTask> dailyTasks, GetDailyTasksQueryParams queryParams)
    {
        List<DailyTask> byDate = dailyTasks
            .Where(d =>
                DateTimeOffset.Compare(queryParams.DateStart.ToDateTimeOffset(), d.Date) <= 0 &&
                DateTimeOffset.Compare(d.Date, queryParams.DateEnd.ToDateTimeOffset()) <= 0)
            .ToList();

        if (queryParams.Progress is not null)
            byDate = byDate.Where(d => d.GetProgress() == queryParams.Progress).ToList();

        return byDate;
    }
    public async Task<Result<DailyTask>> PatchMinutes(int id, PatchDailyTaskRequest body)
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
