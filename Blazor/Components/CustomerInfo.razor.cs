namespace Blazor.Components;

public partial class CustomerInfo
{
    [Inject] public ICustomerService CustomerService { get; set; }
    [Inject] public ISnackbar SnackBar { get; set; }
    private bool hover { get; set; } = true;
    private bool dense { get; set; } = false;
    private string SearchString { get; set; } = String.Empty;
    private Customer customer { get; set; } = new();
    private List<Customer> CustomerList { get; set; }  = new();

    protected override async Task OnInitializedAsync()
    {
        CustomerList = await GetAllCustomer();
    }

    private async Task<List<Customer>> GetAllCustomer()
    {
        return await CustomerService.GetCustomers();
    }


    private async Task Edit(int id)
    {
        customer = CustomerList.FirstOrDefault(c => c.Id == id);
    }
    private async Task Delete(int id)
    {
        await CustomerService.DeleteCustomer(id);
        SnackBar.Add("Customer was deleted", Severity.Success);
        await GetAllCustomer();
    }
    private async Task Save()
    {
        await CustomerService.SaveCustomer(customer);
        customer = new Customer();
        SnackBar.Add("Customer was saved", Severity.Success);
        await GetAllCustomer();
    }

    private bool Search(Customer customer)
    {
        if (customer.FirstName != null && customer.LastName != null && customer.Phone != null &&
            (customer.FirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
             customer.LastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
             customer.Phone.Contains(SearchString, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }
        return false;
    }

}
