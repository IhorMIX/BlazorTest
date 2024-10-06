namespace Blazor.Data;
public class Customer
{
    [Key]

    public int Id { get; set; }
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    [MaxLength(50)]
    public string Phone { get; set; }
}
