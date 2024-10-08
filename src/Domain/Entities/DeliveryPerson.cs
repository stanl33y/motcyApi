using System.ComponentModel.DataAnnotations;

namespace motcyApi.Domain.Entities;

public class DeliveryPerson
{
    public DeliveryPerson()
    {
        Id = string.Empty;
        Name = string.Empty;
        Cnpj = string.Empty;
        DateOfBirth = DateTime.MinValue;
        LicenseNumber = string.Empty;
        LicenseType = string.Empty;
    }
    public DeliveryPerson(string id, string name, string cnpj, DateTime dateOfBirth, string licenseNumber, string licenseType)
    {
        Id = id;
        Name = name;
        Cnpj = cnpj;
        DateOfBirth = dateOfBirth;
        LicenseNumber = licenseNumber;
        LicenseType = licenseType;
    }

    [Key]
    public string Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "CNPJ is required")]
    [StringLength(18, MinimumLength = 18, ErrorMessage = "CNPJ must be exactly 18 characters")]
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

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
