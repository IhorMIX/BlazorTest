namespace Blazor.Components;

public partial class CustomerInfo
{
    [Inject] private ICustomerService CustomerService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] public ISnackbar SnackBar { get; set; }
    private bool Hover { get; set; } = true;
    private bool Dense { get; set; }
    private string SearchString { get; set; } = string.Empty;
    private Customer Customer { get; set; } = new();
    private List<Customer> CustomerList { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        var result = await GetAllCustomerAsync();
        if (!result.Success)
        {
            SnackBar.Add($"Exception about receiving data: {result.Message}", Severity.Error);
            return;
        }
        CustomerList = result.Data;
        StateHasChanged();
    }

    private async Task<OperationResult<List<Customer>>> GetAllCustomerAsync()
    {
        return await CustomerService.GetCustomersAsync();
    }


    private async Task EditAsync(Customer customer)
    {
        var parameters = new DialogParameters<CustomerDialog>
        {
            {"typeDialog", 2},
            {"CustomerInput", customer}
        };

        var dialog = await DialogService.ShowAsync<CustomerDialog>(
            "Сhange of information about customer",
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
        await OnInitializedAsync();
    }

    private async Task ButtonAddClickAsync()
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
        await OnInitializedAsync();
    }
    private async Task DeleteAsync(int id)
    {
        await CustomerService.DeleteCustomerAsync(id);
        SnackBar.Add("Customer was deleted", Severity.Success);
        await OnInitializedAsync();
    }

    private bool Search(Customer customer)
    {
        return customer.FirstName != null && customer.LastName != null && customer.Phone != null &&
            (customer.FirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
             customer.LastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
             customer.Phone.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
    }

}
