namespace Blazor.Repositories;

public class CustomerSerivce : ICustomerService
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerSerivce(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);
        if (customer != null) 
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        return await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Customer>> GetCustomers()
    {
        return await _dbContext.Customers.ToListAsync();
    }

    public async Task SaveCustomer(Customer customer)
    {
        if(customer.Id == 0) 
            await _dbContext.Customers.AddAsync(customer);
        else  
            _dbContext.Customers.Update(customer);
        await _dbContext.SaveChangesAsync();
    }
}
