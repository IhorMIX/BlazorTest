
using Microsoft.EntityFrameworkCore;

namespace Blazor.Data
{
    public class CustomerSerivce : ICustomerService
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerSerivce(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteCustomer(int id)
        {
            var customer = _dbContext.Customers.SingleOrDefault(x => x.Id == id);
            if (customer != null) 
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
            }
        }

        public Customer GetCustomerById(int id)
        {
            return _dbContext.Customers.SingleOrDefault(c => c.Id == id);
        }

        public List<Customer> GetCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public void SaveCustomer(Customer customer)
        {
            if(customer.Id == 0) _dbContext.Customers.Add(customer);
            else _dbContext.Customers.Update(customer);
            _dbContext.SaveChanges();
        }
    }
}
