using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace motcyApi.Domain.Entities;

public class Rental
{
    public Rental()
    {
        Id = string.Empty;
        MotorcycleId = string.Empty;
        DeliveryPersonId = string.Empty;
    }
    public Rental(string id, string motorcycleId, string deliveryPersonId, DateTime startDate, DateTime expectedEndDate, int rentalPlan)
    {
        Id = id;
        MotorcycleId = motorcycleId;
        DeliveryPersonId = deliveryPersonId;
        StartDate = startDate;
        ExpectedEndDate = expectedEndDate;
        RentalPlan = rentalPlan;
    }

    [Key]
    public string Id { get; set; }

    [Required]
    [ForeignKey("Motorcycle")]
    public string MotorcycleId { get; set; }

    [Required]
    [ForeignKey("DeliveryPerson")]
    public string DeliveryPersonId { get; set; }

    [Required(ErrorMessage = "StartDate is required")]
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Required(ErrorMessage = "ExpectedEndDate is required")]
    [DataType(DataType.Date)]
    public DateTime ExpectedEndDate { get; set; }

    [Required(ErrorMessage = "Rental plan is required")]
    public int RentalPlan { get; set; }

    public decimal TotalCost { get; set; }

    public DateTime? ReturnDate { get; set; }

    public Motorcycle? Motorcycle { get; set; }

    public DeliveryPerson? DeliveryPerson { get; set; }
}
