public enum Status { Ok, InvalidData, NotFound, }
public class Result<T>
{
    private string _errorMessage { get; init; } = "";
    private T? _value;
    private Result(T? value, string errorMessage, Status status)
    {
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