namespace motcyApi.Application.DTOs;

/// <summary>
/// Data Transfer Object used for motorcycle rental details.
/// </summary>
public class RentalCreateDTO
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RentalCreateDTO"/> class.
    /// </summary>
    /// <param name="deliveryPersonId">The unique identifier of the delivery person.</param>
    /// <param name="motorcycleId">The unique identifier of the motorcycle.</param>
    /// <param name="startDate">The rental start date.</param>
    /// <param name="rentalPlan">The rental plan.</param>
    public RentalCreateDTO(
        string deliveryPersonId,
        string motorcycleId,
        DateTime startDate,
        int rentalPlan)
    {
        DeliveryPersonId = deliveryPersonId;
        MotorcycleId = motorcycleId;
        StartDate = startDate;
        RentalPlan = rentalPlan;
    }

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
    /// The rental plan.
    /// </summary>
    /// <example>1</example>
    public int RentalPlan { get; set; }
}
