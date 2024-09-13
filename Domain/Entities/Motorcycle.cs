using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Motorcycle
{
    [Key]
    public string Id { get; set; }

    [Required(ErrorMessage = "Year is required")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Model is required")]
    [StringLength(50, ErrorMessage = "Model can't be longer than 50 characters")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Plate is required")]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "Plate must be exactly 7 characters ex.: CDX-0101")]
    public string Plate { get; set; }

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>(); 
}
