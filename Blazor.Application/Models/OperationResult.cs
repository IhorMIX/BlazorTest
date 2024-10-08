namespace Blazor.Application.Models;

public class OperationResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
