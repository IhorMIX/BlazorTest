namespace Blazor.Components.Dialog
{
    public partial class CustomerDialog
    {
        [CascadingParameter] public MudDialogInstance MudDialog { get; set; }
        [Parameter] public int TypeDialog { get; set; }
        [Parameter] public Customer CustomerInput { get; set; } = new();
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private ICustomerService CustomerService { get; set; }

        private string LabelButton { get; set; }
        private Customer Customer { get; set; } = new Customer();
        private MudForm _form;
        private bool _isValid;

        protected override async Task OnInitializedAsync()
        {
            LabelButton = TypeDialog == 1 ? "Add" : "Save";
            if (TypeDialog == 2)
            {
                Customer = CustomerInput;
            }
            await Task.CompletedTask;
        }

        private async Task HandleSubmitAsync()
        {
            await _form.Validate();
            _isValid = _form.IsValid;
            if (_isValid)
            {
                await ActionButtonAsync(Customer);
            }
            else
            {
                Snackbar.Add("Please fill in all required fields correctly.", Severity.Warning);
            }
        }


        private async Task ActionButtonAsync(Customer customer)
        {
            var result = TypeDialog == 1 ? await CustomerAddAsync(customer) : await CustomerEditAsync(customer);

            if (!result.Success)
            {
                Snackbar.Add($"Ошибка: {result.Message}", Severity.Error);
                return;
            }

            Snackbar.Add(TypeDialog == 1 ? "The client has been successfully added" : "Changes have been successfully applied.", Severity.Success);
            MudDialog.Close(DialogResult.Ok(true));
        }

        private async Task<OperationResult<bool>> CustomerAddAsync(Customer customer)
        {
            var result = await CustomerService.AddCustomerAsync(customer);
            return result;
        }

        private async Task<OperationResult<bool>> CustomerEditAsync(Customer customer)
        {
            var result = await CustomerService.EditCustomerAsync(customer);
            return result;
        }
    }
}
