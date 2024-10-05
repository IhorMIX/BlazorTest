namespace Blazor.Interfaces;

public interface ICustomerService
{
    public Task<List<Customer>> GetCustomers();

    public Task<Customer> GetCustomerById(int id);

    public Task SaveCustomer(Customer customer);
    public Task DeleteCustomer(int id);
}
