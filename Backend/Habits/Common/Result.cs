public enum Status { Ok, InvalidData, NotFound, NotOwner }

public class Result<T>
{
    private string? _errorMessage;
    private T? _value;
    private Result(T? value, string? errorMessage, Status status)
    {
        _value = value;
        Status = status;
        _errorMessage = errorMessage;
    }
    public Status Status { get; init; }
    public T Value { get => Status.Equals(Status.Ok) ? _value! : throw new Exception("You can't check this value"); }
    public string ErrorMessage { get => !Status.Equals(Status.Ok) ? _errorMessage! : throw new Exception("You can't check this value"); }
    public static Result<T> Success(T value, Status status = Status.Ok) =>
        new Result<T>(value, default, status);
    public static Result<T> Failure(Status status, string error) =>
        new Result<T>(default, error, status);
}

public static class ResultExtensions
{
    public static IResult ToHttpResponse<T>(this Result<T> result)
    {
        return result.Status switch
        {
            Status.InvalidData => TypedResults.Problem(result.ErrorMessage, statusCode: 400),
            Status.NotOwner => TypedResults.Problem(result.ErrorMessage, statusCode: 401),
            Status.NotFound => TypedResults.Problem(result.ErrorMessage, statusCode: 404),
            _ => TypedResults.Problem(detail: "Ocurrio un error en el servidor", statusCode: 500)
        };
    }
}