namespace Blazor.Interfaces;

public interface ICustomerService
{
    public Task<OperationResult<List<Customer>>> GetCustomersAsync();

    public Task<OperationResult<Customer>> GetCustomerByIdAsync(int id);

    public Task<OperationResult<bool>> EditCustomerAsync(Customer customer);
    public Task<OperationResult<bool>> DeleteCustomerAsync(int id);
    public Task<OperationResult<bool>> AddCustomerAsync(Customer customer);
}
