using Blazor.Components.Dialog;

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


    private async void EditAsync(Customer customer)
    {
        var parameters = new DialogParameters<CustomerDialog>
        {
            {"typeDialog", 2},
            {"Customer", customer}
        };

        var dialog = await DialogService.ShowAsync<CustomerDialog>(
            parameters: parameters,
            new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            });

        var res = await dialog.Result;
        if (res.Canceled)
        {
            return;
        }
        await GetAllCustomerAsync();
    }

    private async void ButtonAddClickAsync()
    {
        var parameters = new DialogParameters<CustomerDialog>
        {
            {"typeDialog", 1},
        };

        var dialog = await DialogService.ShowAsync<CustomerDialog>(
            "Add customer",
            parameters: parameters,
            new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            });

        var res = await dialog.Result;
        if (res.Canceled)
        {
            return;
        }
        await GetAllCustomerAsync();
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
