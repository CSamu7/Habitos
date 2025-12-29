using Habits.Features.Tasks;
using Habits.Features.Tasks.Validations;
using Habits.Models;
using Microsoft.EntityFrameworkCore;
public class TaskFilters(DateOnly? start, DateOnly? end, DailyTaskProgress? progress)
{
    private DateTimeOffset _dateStart;
    private DateTimeOffset _dateEnd;
    public DateTimeOffset DateStart
    {
        get => _dateStart; 
        set 
        {
            _dateStart = (start is not null
                ? new DateTimeOffset(start.Value, new TimeOnly(24, 0), new TimeSpan(0))
                : DateTimeOffset.Now - new TimeSpan(24, 0, 0));
        }
    }
    public DateTimeOffset DateEnd 
    { 
        get => _dateEnd;
        set
        {
            _dateEnd = (end is not null
                ? new DateTimeOffset(end.Value, new TimeOnly(24, 0), new TimeSpan(0))
                : DateTimeOffset.Now);
        }
    }
    public DailyTaskProgress? Progress { get; set; } = progress;
}

public enum Status { Ok, InvalidData, NotFound, }
public class Result<T>
{
    private string _errorMessage { get; init; } = "";
    private T? _value;
    private Result(T? value, string errorMessage, Status status){
        _value = value;
        Status = status;
        _errorMessage = errorMessage;
    }
    public Status Status { get; init; }
    public T Value { get => Status.Equals(Status.Ok) ? _value! : throw new Exception("You can't check this value"); }
    public string ErrorMessage { get => !Status.Equals(Status.Ok) ? _errorMessage : throw new Exception("You can't check this value"); }
    public static Result<T> Success(T value, Status status = Status.Ok) => new Result<T>(value, "", status);
    public static Result<T> Failure(Status status, string message) => new Result<T>(default, message, status); 
}

public static class ResultExtensions
{
    //FIX: No podemos mandar varios errores.
    public static IResult ToProblem<T>(this Result<T> result)
    {
        return result.Status switch
        {
            Status.Ok => TypedResults.Problem(detail: result.ErrorMessage, statusCode: 200),
            Status.InvalidData => TypedResults.Problem(detail: result.ErrorMessage, statusCode: 400),
            Status.NotFound => TypedResults.Problem(detail: result.ErrorMessage, statusCode: 404)
        };
    }
}
public class DailyTaskService
{
    private HabitsContext _db;
    public DailyTaskService(HabitsContext db)
    {
        _db = db;
    }

    public List<DailyTask> GetDailyTask(int user, TaskFilters filters)
    {
        var dailyTasks = _db.DailyTasks
            .Include(dailyTask => dailyTask.IdTaskNavigation)
            .Where(dailyTask => dailyTask.Date < filters.DateEnd && filters.DateStart > dailyTask.Date)
            .ToList();

        if (filters.Progress is null)
            return dailyTasks;

        return dailyTasks
            .Where(d => d.GetProgress() == filters.Progress)
            .ToList();
    }
    //TODO: Si el filtro de progreso es nulo, entonces traer a todos.
    public List<DailyTask> GetDailyTasks(int idUser, TaskFilters filters)
    {
        var dailyTasks = _db.DailyTasks
            .Include(dailyTask => dailyTask.IdTaskNavigation)
            .Where(dailyTask => dailyTask.Date < filters.DateEnd && filters.DateStart > dailyTask.Date)
            .ToList();

        if (filters.Progress is null)
            return dailyTasks;

        return dailyTasks
            .Where(d => d.GetProgress() == filters.Progress)
            .ToList();
    }
    public async Task<Result<DailyTask>> PatchMinutes(int id, PatchDailyTask body, Action<DailyTask, PatchDailyTask> func)
    {
        DailyTask? dailyTask = await _db.DailyTasks.FindAsync(id);
        DailyTaskValidation validation = new DailyTaskValidation(dailyTask);

        var result = validation.Validate();

        if (!result.Status.Equals(Status.Ok)) return result;

        func(result.Value, body);
        _db.SaveChanges();

        return Result<DailyTask>.Success(result.Value);
    }
    //No se si esto va en otra parte pero por el momento se quedara aqui.
    //No cumple con la responsabilidad de esta clase que es interactuar con la base de datos pero
    //esta relacionado, entonces por el momento se quedara...
    public void AddMinutes(DailyTask dailyTask, PatchDailyTask body)
    {
        dailyTask.MinutesCompleted += body.Minutes;

        if (dailyTask.MinutesCompleted >= dailyTask.TotalMinutes &&
            dailyTask.CompletedAt is null)
            dailyTask.CompletedAt = body.Time;
    }
    public void ReplaceMinutes(DailyTask dailyTask, PatchDailyTask body)
    {
        if (body.Minutes < dailyTask.TotalMinutes)
        {
            dailyTask.CompletedAt = null;
        } else
        {
            dailyTask.CompletedAt = body.Time;
        }

        dailyTask.MinutesCompleted = body.Minutes;
    }
}