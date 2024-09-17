/// <summary>
/// Data Transfer Object used for motorcycle rental details.
/// </summary>
public class RentalDTO
{
    /// <summary>
    /// The unique identifier of the rental.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// The unique identifier of the delivery person.
    /// </summary>
    /// <example>123</example>
    public string EntregadorId { get; set; }

    /// <summary>
    /// The unique identifier of the motorcycle.
    /// </summary>
    /// <example>456</example>
    public string MotoId { get; set; }

    /// <summary>
    /// The rental start date.
    /// </summary>
    /// <example>2024-09-01T12:00:00Z</example>
    public DateTime DataInicio { get; set; }

    /// <summary>
    /// The rental end date.
    /// </summary>
    /// <example>2024-09-10T12:00:00Z</example>
    public DateTime? DataTermino { get; set; }

    /// <summary>
    /// The estimated end date for the rental.
    /// </summary>
    /// <example>2024-09-08T12:00:00Z</example>
    public DateTime DataPrevisaoTermino { get; set; }

    /// <summary>
    /// The rental plan.
    /// </summary>
    /// <example>1</example>
    public int Plano { get; set; }

    /// <summary>
    /// The total value of the rental.
    /// </summary>
    public decimal? ValorTotal { get; set; }
}
