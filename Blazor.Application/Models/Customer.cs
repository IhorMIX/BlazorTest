namespace Blazor.Data;
public class Customer
{
    [Key]

    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }

    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
    public string Phone { get; set; }
}
