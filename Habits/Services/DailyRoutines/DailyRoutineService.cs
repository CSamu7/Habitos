using Habits.API.DailyRoutines.DTO;
using Habits.Common;
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
    public DailyRoutineService(HabitsContext db, UserManager<User> userManager)
    {
        _userManager = userManager;
        _db = db;
    }
    public async Task<Result<DailyRoutine>> GetRoutine(int idDailyRoutine)
    {
        var dailyTask = await _db.DailyRoutines
            .Include(d => d.IdRoutineNavigation.IdUserNavigation)
            .FirstOrDefaultAsync(d => d.IdDailyRoutine == idDailyRoutine);

        if (dailyTask is null) return Result<DailyRoutine>.Failure(Status.NotFound, "Daily task doesn't exist");

        return Result<DailyRoutine>.Success(dailyTask);
    }
    public async Task<Result<List<DailyRoutine>>> GetRoutines(string username, GetDailyRoutineQueryParams queryParams)
    {
        User? user = await _userManager.FindByNameAsync(username);

        var dailyTasks = _db.DailyRoutines
            .Include(d => d.IdRoutineNavigation)
            .Where(d => d.IdRoutineNavigation.IdUser == user.Id)
            .AsNoTracking()
            .ToList();

        List<DailyRoutine> filtered = Filter(dailyTasks, queryParams);

        return Result<List<DailyRoutine>>.Success(filtered);
    }
    private List<DailyRoutine> Filter(List<DailyRoutine> dailyTasks, GetDailyRoutineQueryParams queryParams)
    {
        List<DailyRoutine> byDate = dailyTasks
            .Where(d =>
                DateTimeOffset.Compare(queryParams.DateStart.ToDateTimeOffset(), d.Date) <= 0 &&
                DateTimeOffset.Compare(d.Date, queryParams.DateEnd.ToDateTimeOffset()) <= 0)
            .ToList();

        if (queryParams.Progress is not null)
            byDate = byDate.Where(d => d.GetProgress() == queryParams.Progress).ToList();

        return byDate;
    }
    public async Task<Result<DailyRoutine>> PatchMinutes(int id, PatchDailyRoutineRequest body)
    {
        DailyRoutine? dailyTask = await _db.DailyRoutines.FindAsync(id);

        if (dailyTask is null)
            return Result<DailyRoutine>.Failure(Status.NotFound, "Daily dask doesn't exist");

        PatchValidation validation = new PatchValidation();
        Result<DailyRoutine> result = validation.Validate(dailyTask);
        if (!result.Status.Equals(Status.Ok)) return result;

        IDailyTaskPatchCommand command = GetPatchCommand(body.Operation);

        command.ChangeMinutes(dailyTask, body);
        _db.SaveChanges();

        return Result<DailyRoutine>.Success(result.Value);
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
