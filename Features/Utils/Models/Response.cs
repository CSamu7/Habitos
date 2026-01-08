
//FIX: ResponseList<T>
public record ResponseBase<T>(
    List<T> Results,
    int Total
);