using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Rental
{
    [Key]
    public int Id { get; set; }

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


    public Motorcycle Motorcycle { get; set; }

    public DeliveryPerson DeliveryPerson { get; set; }
}

