namespace Blazor.Components;

public partial class CustomerInfo
{
    [Inject] public ICustomerService customerService { get; set; }
    [Inject] public ISnackbar snackBar { get; set; }
    private bool hover { get; set; } = true;
    private bool dense { get; set; } = false;
    private string searchString { get; set; } = String.Empty;
    private Customer customer = new Customer();
    private List<Customer> customers = new List<Customer>();

    protected override async Task OnInitializedAsync()
    {
        GetAllCustomer();
    }

    private List<Customer> GetAllCustomer()
    {
        return customerService.GetCustomers();
    }


    private void Edit(int id)
    {
        customer = customers.FirstOrDefault(c => c.Id == id);
    }
    private void Delete(int id)
    {
        customerService.DeleteCustomer(id);
        snackBar.Add("Customer was deleted", Severity.Success);
        GetAllCustomer();
    }
    private void Save()
    {
        customerService.SaveCustomer(customer);
        customer = new Customer();
        snackBar.Add("Customer was saved", Severity.Success);
        GetAllCustomer();
    }

    private bool Search(Customer customer)
    {
        if (customer.FirstName != null && customer.LastName != null && customer.Phone != null &&
            (customer.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
             customer.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
             customer.Phone.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }
        return false;
    }

}
