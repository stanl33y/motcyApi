using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsible for managing motorcycle rentals.
/// </summary>
[ApiController]
[Route("api/v1/locacao")]
public class RentalController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    /// <summary>
    /// Creates a new rental for a motorcycle.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/v1/locacao
    ///     {
    ///        "entregador_id": "123",
    ///        "moto_id": "456",
    ///        "plano": 1,
    ///        "data_inicio": "2024-09-01T12:00:00Z",
    ///        "data_termino": "2024-09-10T12:00:00Z",
    ///        "data_previsao_termino": "2024-09-08T12:00:00Z"
    ///     }
    /// 
    /// Creates a new rental with the provided details.
    /// </remarks>
    /// <param name="rentalDto">Object containing rental details.</param>
    /// <returns>Returns the created rental or an error message.</returns>
    /// <response code="201">Rental successfully created</response>
    /// <response code="400">Error creating rental</response>
    [Authorize(Roles = "admin, deliveryperson")]
    [HttpPost]
    public async Task<IActionResult> CreateRental([FromBody] RentalDTO rentalDto)
    {
        var rental = await _rentalService.CreateRentalAsync(
            rentalDto.MotoId,
            rentalDto.EntregadorId,
            rentalDto.Plano,
            rentalDto.DataInicio,
            rentalDto.DataTermino,
            rentalDto.DataPrevisaoTermino);

        if (rental == null)
        {
            return BadRequest("Error creating rental.");
        }

        return CreatedAtAction(nameof(GetRentalById), new { id = rental.Id }, rental);
    }

    /// <summary>
    /// Marks the return of a rented motorcycle.
    /// </summary>
    /// <param name="id">The rental ID.</param>
    /// <param name="returnDate">Object containing the return date.</param>
    /// <returns>Returns the updated rental or 404 if not found.</returns>
    /// <response code="200">Rental successfully updated with return date</response>
    /// <response code="404">Rental not found</response>
    [Authorize(Roles = "admin, deliveryperson")]
    [HttpPut("{id}/devolucao")]
    public async Task<IActionResult> ReturnMotorcycle(int id, [FromBody] ReturnDateDTO returnDate)
    {
        var rental = await _rentalService.ReturnMotorcycleAsync(id, returnDate.DataDevolucao);
        if (rental == null)
        {
            return NotFound();
        }

        return Ok(rental);
    }

    /// <summary>
    /// Retrieves a rental by its ID.
    /// </summary>
    /// <param name="id">The rental ID.</param>
    /// <returns>Returns the rental details or 404 if not found.</returns>
    /// <response code="200">Rental details retrieved successfully</response>
    /// <response code="404">Rental not found</response>
    [Authorize(Roles = "admin, deliveryperson")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRentalById(int id)
    {
        var rental = await _rentalService.GetRentalByIdAsync(id);
        if (rental == null)
        {
            return NotFound();
        }

        return Ok(rental);
    }

    /// <summary>
    /// Retrieves all rentals.
    /// </summary>
    /// <returns>Returns a list of all rentals.</returns>
    /// <response code="200">List of rentals retrieved successfully</response>
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllRentals()
    {
        var rentals = await _rentalService.GetAllRentalsAsync();
        return Ok(rentals);
    }
}
