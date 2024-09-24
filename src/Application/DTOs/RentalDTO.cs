namespace motcyApi.Application.DTOs;

/// <summary>
/// Data Transfer Object used for motorcycle rental details.
/// </summary>
public class RentalDTO
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RentalDTO"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the rental.</param>
    /// <param name="deliveryPersonId">The unique identifier of the delivery person.</param>
    /// <param name="motorcycleId">The unique identifier of the motorcycle.</param>
    /// <param name="startDate">The rental start date.</param>
    /// <param name="endDate">The rental end date.</param>
    /// <param name="expectedEndDate">The estimated end date for the rental.</param>
    /// <param name="rentalPlan">The rental plan.</param>
    /// <param name="totalCost">The total value of the rental.</param>
    public RentalDTO(int? id, string deliveryPersonId, string motorcycleId, DateTime startDate, DateTime? endDate,
        DateTime expectedEndDate, int rentalPlan, decimal totalCost)
    {
        Id = id;
        DeliveryPersonId = deliveryPersonId;
        MotorcycleId = motorcycleId;
        StartDate = startDate;
        EndDate = endDate;
        ExpectedEndDate = expectedEndDate;
        RentalPlan = rentalPlan;
        TotalCost = totalCost;
    }

    /// <summary>
    /// The unique identifier of the rental.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// The unique identifier of the delivery person.
    /// </summary>
    /// <example>123</example>
    public string DeliveryPersonId { get; set; }

    /// <summary>
    /// The unique identifier of the motorcycle.
    /// </summary>
    /// <example>456</example>
    public string MotorcycleId { get; set; }

    /// <summary>
    /// The rental start date.
    /// </summary>
    /// <example>2024-09-01T12:00:00Z</example>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The rental end date.
    /// </summary>
    /// <example>2024-09-10T12:00:00Z</example>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// The estimated end date for the rental.
    /// </summary>
    /// <example>2024-09-08T12:00:00Z</example>
    public DateTime ExpectedEndDate { get; set; }

    /// <summary>
    /// The rental plan.
    /// </summary>
    /// <example>1</example>
    public int RentalPlan { get; set; }

    /// <summary>
    /// The total value of the rental.
    /// </summary>
    public decimal TotalCost { get; set; }

}
