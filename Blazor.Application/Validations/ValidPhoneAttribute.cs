namespace Blazor.Application.Validations;

public class ValidPhoneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var phone = value as string;

        if (string.IsNullOrEmpty(phone))
        {
            return new ValidationResult("Phone number is required.");
        }

        if (phone.All(char.IsDigit) && phone.Length == 10)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Phone number must contain exactly 10 digits.");
    }
}
