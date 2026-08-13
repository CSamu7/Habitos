using Habits.API.DailyRoutines.DTO;
using Habits.Features.DailyTasks.Models;
using Habits.Models;
using Habits.Services.DailyRoutines;
using Habits.Services.DailyTasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
public class DailyRoutineService
{
    private HabitsContext _db;
    private UserManager<User> _userManager;
    private TimeProvider _timeProvider;
    public DailyRoutineService(HabitsContext db, UserManager<User> userManager, TimeProvider timeProvider)
    {
        _userManager = userManager;
        _db = db;
        _timeProvider = timeProvider;
    }
    public async Task<Result<DailyTask>> GetRoutine(int idDailyRoutine)
    {
        var dailyTask = await _db.DailyTasks
            .Include(d => d.IdRoutineNavigation.IdUser)
            .FirstOrDefaultAsync(d => d.IdDailyTask == idDailyRoutine);

        if (dailyTask is null) return Result<DailyTask>.Failure(Status.NotFound, "No existe la tarea diaria");

        return Result<DailyTask>.Success(dailyTask);
    }
    public async Task<Result<List<DailyTask>>> GetRoutines(string username, GetDailyRoutineQueryParams queryParams)
    {
        User? user = await _userManager.FindByNameAsync(username);

        if (user is null) return Result<List<DailyTask>>.Failure(Status.NotOwner, "Problemas de credenciales");

        var dailyTasks = _db.DailyTasks
            .Include(d => d.IdRoutineNavigation)
            .Where(d => d.IdRoutineNavigation.IdUser == user.Id)
            .AsNoTracking()
            .ToList();

        AdjustDateRangeToCutoffTime(user.CutOffTime, queryParams);
        List<DailyTask> tasksSelected = Filter(dailyTasks, queryParams);

        return Result<List<DailyTask>>.Success(tasksSelected);
    }

    private void AdjustDateRangeToCutoffTime(int cutOffTime, GetDailyRoutineQueryParams queryParams)
    {
        queryParams.DateStart = queryParams.DateStart.AddHours(cutOffTime);
        queryParams.DateEnd = queryParams.DateEnd.AddHours(cutOffTime);

        int actualHour = _timeProvider.GetUtcNow().Hour;

        if (actualHour > cutOffTime)
        {
            queryParams.DateStart = queryParams.DateStart.AddDays(1);
            queryParams.DateEnd = queryParams.DateEnd.AddDays(1);
        }
    }
    private List<DailyTask> Filter(List<DailyTask> dailyTasks, GetDailyRoutineQueryParams queryParams)
    {
        List<DailyTask> byDate = dailyTasks
            .Where(d =>
                DateTimeOffset.Compare(queryParams.DateStart, d.Date) <= 0 &&
                DateTimeOffset.Compare(d.Date, queryParams.DateEnd) < 0)
            .ToList();

        if (queryParams.Progress.Any())
            byDate = byDate.Where(d => queryParams.Progress.Contains(d.GetProgress())).ToList();

        return byDate;
    }
    public async Task<Result<DailyTask>> PatchMinutes(int id, PatchDailyRoutineRequest body)
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
            PatchOperations.Replace => new OverwriteMinutes(),
            _ => throw new Exception("Esta operación no existe")
        };
    }
}
