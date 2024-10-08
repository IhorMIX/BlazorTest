using Blazor.Application.Validations;

namespace Blazor.Data;
public class Customer
{
    [Key]

    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    [ValidPhone]
    public string Phone { get; set; }
}
