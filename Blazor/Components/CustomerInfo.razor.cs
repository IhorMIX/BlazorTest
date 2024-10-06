namespace Blazor.Components;

public partial class CustomerInfo
{
    [Inject] public ICustomerService CustomerService { get; set; }
    [Inject] public ISnackbar SnackBar { get; set; }
    private bool Hover { get; set; } = true;
    private bool Dense { get; set; }
    private string SearchString { get; set; } = string.Empty;
    private Customer Customer { get; set; } = new();
    private List<Customer> CustomerList { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        CustomerList = await GetAllCustomerAsync();
    }

    private async Task<List<Customer>> GetAllCustomerAsync()
    {
        return await CustomerService.GetCustomersAsync();
    }


    private void Edit(int id)
    {
        Customer = CustomerList.FirstOrDefault(c => c.Id == id);
    }
    private async Task DeleteAsync(int id)
    {
        await CustomerService.DeleteCustomerAsync(id);
        SnackBar.Add("Customer was deleted", Severity.Success);
        await GetAllCustomerAsync();
    }
    private async Task SaveAsync()
    {
        await CustomerService.SaveCustomerAsync(Customer);
        Customer = new Customer();
        SnackBar.Add("Customer was saved", Severity.Success);
        await GetAllCustomerAsync();
    }

    private bool Search(Customer customer)
    {
        return customer.FirstName != null && customer.LastName != null && customer.Phone != null &&
            (customer.FirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
             customer.LastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
             customer.Phone.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
    }

}
