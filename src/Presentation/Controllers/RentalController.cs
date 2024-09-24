using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using motcyApi.Application.DTOs;
using motcyApi.Application.DTOs.Validators;
using motcyApi.Application.Interfaces;
using motcyApi.Domain.Entities;

namespace motcyApi.Presentation.Controllers;

/// <summary>
/// Controller responsible for managing motorcycle rentals.
/// </summary>
[ApiController]
[Route("api/v1/rental")]
public class RentalController : ControllerBase
{
    private readonly IRentalService _rentalService;
    private readonly IMapper _mapper;

    public RentalController(IRentalService rentalService, IMapper mapper)
    {
        _rentalService = rentalService;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new rental for a motorcycle.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/rental
    ///     {
    ///        "deliveryperson_id": "123",
    ///        "motorcycle_id": "456",
    ///        "rental_plan": 1,
    ///        "start_date": "2024-09-01T12:00:00Z",
    ///        "end_date": null,
    ///        "expected_end_date": "2024-09-08T13:00:00Z"
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
    public async Task<ActionResult<RentalDTO>> CreateRental([FromBody] RentalCreateDTO rentalDto)
    {
        var validator = new RentalCreateDTOValidator();

        var results = validator.Validate(rentalDto);

        if (!results.IsValid)
        {
            var errors = results.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(new { errors });
        }

        var rental = _mapper.Map<RentalCreateDTO, Rental>(rentalDto);

        rental.Id = Guid.NewGuid().ToString();
        rental.ExpectedEndDate = rental.StartDate.AddDays(rental.RentalPlan);

        var result = await _rentalService.CreateRentalAsync(rental);

        if (result == null)
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
    [HttpPut("{id}/return")]
    public async Task<ActionResult<RentalDTO>> ReturnMotorcycle(string id, [FromBody] ReturnDateDTO returnDate)
    {
        var rental = await _rentalService.ReturnMotorcycleAsync(id, returnDate.ReturnDate);
        if (rental == null)
        {
            return NotFound();
        }

        var rentalDto = _mapper.Map<Rental, RentalDTO>(rental);

        return Ok(rentalDto);
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
    public async Task<ActionResult<RentalDTO>> GetRentalById(string id)
    {
        var rental = await _rentalService.GetRentalByIdAsync(id);
        if (rental == null)
        {
            return NotFound();
        }

        var rentalDto = _mapper.Map<Rental, RentalDTO>(rental);

        return Ok(rentalDto);
    }

    /// <summary>
    /// Retrieves all rentals.
    /// </summary>
    /// <returns>Returns a list of all rentals.</returns>
    /// <response code="200">List of rentals retrieved successfully</response>
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<List<RentalDTO>>> GetAllRentals()
    {
        var rentals = await _rentalService.GetAllRentalsAsync();

        if (rentals == null)
        {
            return new List<RentalDTO>();
        }

        var rentalsDto = _mapper.Map<List<Rental>, List<RentalDTO>>(rentals.ToList());

        return Ok(rentalsDto);
    }
}
