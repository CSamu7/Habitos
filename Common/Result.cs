public enum Status { Ok, InvalidData, NotFound, }
public class Result<T>
{
    private string _message { get; init; } = "";
    private T? _value;
    private Result(T? value, string errorMessage, Status status)
    {
        _value = value;
        Status = status;
        _message = errorMessage;
    }
    public Status Status { get; init; }
    public T Value { get => Status.Equals(Status.Ok) ? _value! : throw new Exception("You can't check this value"); }
    public string ErrorMessage { get => !Status.Equals(Status.Ok) ? _message : throw new Exception("You can't check this value"); }
    public static Result<T> Success(T value, Status status = Status.Ok) => new Result<T>(value, "", status);
    public static Result<T> Failure(Status status, string message) => new Result<T>(default, message, status);
}
public static class ResultExtensions
{
    public static IResult ToHttpResponse<T>(this Result<T> result)
    {
        return result.Status switch
        {
            Status.InvalidData => TypedResults.Problem(result.ErrorMessage, statusCode: 400),
            Status.NotFound => TypedResults.Problem(result.ErrorMessage, statusCode: 404),
            _ => TypedResults.Problem(detail: "something went wrong", statusCode: 500)
        };
    }
}