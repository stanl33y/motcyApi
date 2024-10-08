using System.ComponentModel.DataAnnotations;

namespace motcyApi.Domain.Entities;

public class Motorcycle
{
    public Motorcycle()
    {
        Id = string.Empty;
        Model = string.Empty;
        Plate = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Motorcycle"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the motorcycle.</param>
    /// <param name="year">The year the motorcycle was manufactured.</param>
    /// <param name="model">The model of the motorcycle.</param>
    /// <param name="plate">The license plate of the motorcycle.</param>
    public Motorcycle(string id, int year, string model, string plate)
    {
        Id = id;
        Year = year;
        Model = model;
        Plate = plate;
    }

    [Key]
    public string Id { get; set; }

    [Required(ErrorMessage = "Year is required")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Model is required")]
    [StringLength(50, ErrorMessage = "Model can't be longer than 50 characters")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Plate is required")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "Plate must be exactly 8 characters ex.: CDX-0101")]
    public string Plate { get; set; }

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
