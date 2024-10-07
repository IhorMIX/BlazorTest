namespace Blazor.Components.Dialog;

public partial class CustomerDialog
{
    [CascadingParameter] public MudDialogInstance MudDialog { get; set; }
    [Parameter] public int TypeDialog { get; set; }
    [Parameter] public Customer CustomerInput { get; set; } = new();
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private ICustomerService CustomerService { get; set; }
    private string LabelButton { get; set; }

    private Customer Customer { get; set; } = new Customer();

    protected override async Task OnInitializedAsync()
    {
        LabelButton = TypeDialog == 1 ? "Add" : "Save";
        if (TypeDialog == 2)
        {
            Customer = CustomerInput;
        }
        await Task.CompletedTask;
    }

    private async Task ActionButtonAsync(Customer customer)
    {
        if (TypeDialog == 1)
        {
            await CustomerAddAsync(customer);
        }
        else
        {
            await CustomerEditAsync(customer);
        }
        MudDialog.Close(DialogResult.Ok(true));
    }

    private async Task CustomerAddAsync(Customer customer)
    {
        await CustomerService.SaveCustomerAsync(customer);
        Snackbar.Add("Change of info about customer", Severity.Success);
    }

    private async Task CustomerEditAsync(Customer customer)
    {
        await CustomerService.SaveCustomerAsync(customer);
        Snackbar.Add("Change of info about customer", Severity.Success);
    }
}
