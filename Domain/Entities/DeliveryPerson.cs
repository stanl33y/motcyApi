using System.ComponentModel.DataAnnotations;

public class DeliveryPerson
{
    [Key]
    public string Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "CNPJ is required")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "CNPJ must be exactly 14 characters")]
    public string Cnpj { get; set; }

    [Required(ErrorMessage = "DateOfBirth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "LicenseNumber is required")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "LicenseNumber must be exactly 11 characters")]
    public string LicenseNumber { get; set; }

    [Required(ErrorMessage = "LicenseType is required")]
    [StringLength(3, ErrorMessage = "LicenseType can only be 'A', 'B', or 'A+B'")]
    public string LicenseType { get; set; }

    [Required(ErrorMessage = "LicenseImage is required")]
    public string LicenseImage { get; set; }

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
