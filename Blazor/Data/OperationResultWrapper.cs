namespace Blazor.Data;

public static class OperationResultWrapper
{
    public static OperationResult<T> Success<T>(T data, string message = "Success")
    {
        return new OperationResult<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static OperationResult<T> Failure<T>(string message, T data = default)
    {
        return new OperationResult<T>
        {
            Success = false,
            Data = data,
            Message = message
        };
    }
}
