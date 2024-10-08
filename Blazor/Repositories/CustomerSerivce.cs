namespace Blazor.Repositories;

public class CustomerSerivce(ApplicationDbContext dbContext) : ICustomerService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<OperationResult<bool>> AddCustomerAsync(Customer customer)
    {
        try
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return OperationResultWrapper.Success(true, "A client successfully added");
        }
        catch (Exception ex)
        {
            return OperationResultWrapper.Failure<bool>($"Exception about adding a customer {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> DeleteCustomerAsync(int id)
    {
        try
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
                return OperationResultWrapper.Success(true, "A client successfully removed");
            }
            else
            {
                return OperationResultWrapper.Failure<bool>("A client not found");
            }
        }
        catch (Exception ex)
        {
            return OperationResultWrapper.Failure<bool>($"{ex.Message}");
        }
    }

    public async Task<OperationResult<Customer>> GetCustomerByIdAsync(int id)
    {
        try
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);
            return customer != null
                ? OperationResultWrapper.Success(customer, "A client successfully find")
                : OperationResultWrapper.Failure<Customer>("A client not found");
        }
        catch (Exception ex)
        {
            return OperationResultWrapper.Failure<Customer>($"Exception about receiving a customer {ex.Message}");
        }
    }

    public async Task<OperationResult<List<Customer>>> GetCustomersAsync()
    {
        try
        {
            var customers = await _dbContext.Customers.ToListAsync();
            return OperationResultWrapper.Success(customers, "The list of clients successfully receive");
        }
        catch (Exception ex)
        {
            return OperationResultWrapper.Failure<List<Customer>>($"Exception about receiving a list of customers {ex.Message}");
        }
    }

    public async Task<OperationResult<bool>> EditCustomerAsync(Customer customer)
    {
        try
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
            return OperationResultWrapper.Success(true, "Changes successfully saved");
        }
        catch (Exception ex)
        {
            return OperationResultWrapper.Failure<bool>($"Exception about saving changes {ex.Message}");
        }
    }
}
