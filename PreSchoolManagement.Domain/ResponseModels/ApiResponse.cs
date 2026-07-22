namespace PreSchoolManagement.Domain.ResponseModels;
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> SuccessResponse(T? data, string message = "Success", int statusCode = 200)
        => new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = statusCode
        };

    public static ApiResponse<T> FailureResponse(string message, int statusCode = 500,T? data = default)
    => new ApiResponse<T>
    {
        Success = false,
        Message = message,
        StatusCode = statusCode,
        Data = data
    };

    public static ApiResponse<int> SuccessResponse(object menuId, object value, int oK)
    {
        throw new NotImplementedException();
    }
}
