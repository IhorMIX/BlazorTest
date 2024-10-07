namespace Blazor.Interfaces;

public interface ICustomerService
{
    public Task<List<Customer>> GetCustomersAsync();

    public Task<Customer> GetCustomerByIdAsync(int id);

    public Task SaveCustomerAsync(Customer customer);
    public Task DeleteCustomerAsync(int id);
    public Task AddCustomerAsync(Customer customer);
}
