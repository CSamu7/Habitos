public record BaseResponse<T>(
    List<T> Results,
    int Total
);