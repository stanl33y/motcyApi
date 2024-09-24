/// <summary>
/// Data Transfer Object used for motorcycle registration and details.
/// </summary>
public class MotorcycleDTO
{

    /// <summary>
    /// Initializes a new instance of the <see cref="MotorcycleDTO"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the motorcycle.</param>
    /// <param name="year">The year the motorcycle was manufactured.</param>
    /// <param name="model">The model of the motorcycle.</param>
    /// <param name="plate">The license plate of the motorcycle.</param>
    public MotorcycleDTO(string id, int year, string model, string plate)
    {
        Id = id;
        Year = year;
        Model = model;
        Plate = plate;
    }

    /// <summary>
    /// The unique identifier of the motorcycle.
    /// </summary>
    /// <example>123</example>
    public string Id { get; set; }

    /// <summary>
    /// The year the motorcycle was manufactured.
    /// </summary>
    /// <example>2020</example>
    public int Year { get; set; }

    /// <summary>
    /// The model of the motorcycle.
    /// </summary>
    /// <example>Yamaha XTZ</example>
    public string Model { get; set; }

    /// <summary>
    /// The license plate of the motorcycle.
    /// </summary>
    /// <example>XYZ-1234</example>
    public string Plate { get; set; }
}
