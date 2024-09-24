using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using motcyApi.Application.DTOs;
using motcyApi.Application.DTOs.Validators;
using motcyApi.Application.Interfaces;
using motcyApi.Domain.Entities;

namespace motcyApi.Presentation.Controllers;

/// <summary>
/// Controller responsible for managing motorcycles.
/// </summary>
[ApiController]
[Route("api/v1/motorcycles")]
public class MotorcycleController : ControllerBase
{
    private readonly IMotorcycleService _motorcycleService;
    private readonly IMapper _mapper;

    public MotorcycleController(IMotorcycleService motorcycleService, IMapper mapper)
    {
        _motorcycleService = motorcycleService;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new motorcycle.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/motorcycles
    ///     {
    ///        "year": 2020,
    ///        "model": "Yamaha XTZ",
    ///        "plate": "XYZ-1234"
    ///     }
    ///
    /// Creates a new motorcycle with the provided details.
    /// </remarks>
    /// <param name="motorcycleDto">Object containing motorcycle details.</param>
    /// <returns>Returns the created motorcycle or an error message.</returns>
    /// <response code="201">Motorcycle successfully created</response>
    /// <response code="400">Error creating motorcycle</response>
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateMotorcycle([FromBody] MotorcycleCreateDTO motorcycleDto)
    {
        var validator = new MotorcycleCreateDTOValidator();

        var results = validator.Validate(motorcycleDto);

        if (!results.IsValid)
        {
            var errors = results.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(new { errors });
        }

        var motorcycle = _mapper.Map<MotorcycleCreateDTO, Motorcycle>(motorcycleDto);

        motorcycle.Id = Guid.NewGuid().ToString();

        var result = await _motorcycleService.AddMotorcycleAsync(motorcycle);

        if (result == null)
        {
            return BadRequest("Error creating motorcycle.");
        }

        return CreatedAtAction(nameof(GetMotorcycleById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Retrieves a motorcycle by its ID.
    /// </summary>
    /// <param name="id">The motorcycle ID.</param>
    /// <returns>Returns the motorcycle details or 404 if not found.</returns>
    /// <response code="200">Motorcycle details retrieved successfully</response>
    /// <response code="404">Motorcycle not found</response>
    [Authorize(Roles = "admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<MotorcycleDTO>> GetMotorcycleById(string id)
    {
        var motorcycle = await _motorcycleService.GetMotorcycleByIdAsync(id);
        if (motorcycle == null)
        {
            return NotFound();
        }

        var motorcycleDto = _mapper.Map<Motorcycle, MotorcycleDTO>(motorcycle);

        return Ok(motorcycleDto);
    }

    /// <summary>
    /// Updates a motorcycle's plate by its ID.
    /// </summary>
    /// <param name="id">The motorcycle ID.</param>
    /// <param name="motorcycleDto">Object containing updated motorcycle plate information.</param>
    /// <returns>Returns the updated motorcycle or 404 if not found.</returns>
    /// <response code="200">Motorcycle successfully updated</response>
    /// <response code="404">Motorcycle not found</response>
    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMotorcycle(string id, [FromBody] MotorcycleDTO motorcycleDto)
    {
        var updatedMotorcycle = await _motorcycleService.UpdateMotorcyclePlateAsync(id, motorcycleDto.Plate);
        if (updatedMotorcycle == null)
        {
            return NotFound();
        }

        var motorcycleDtoUpdated = _mapper.Map<Motorcycle, MotorcycleDTO>(updatedMotorcycle);

        return Ok(motorcycleDtoUpdated);
    }

    /// <summary>
    /// Deletes a motorcycle by its ID.
    /// </summary>
    /// <param name="id">The motorcycle ID.</param>
    /// <returns>Returns 204 on success or 404 if the motorcycle is not found.</returns>
    /// <response code="204">Motorcycle successfully deleted</response>
    /// <response code="404">Motorcycle not found</response>
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMotorcycle(string id)
    {
        var deleted = await _motorcycleService.DeleteMotorcycleAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Retrieves all motorcycles.
    /// </summary>
    /// <returns>Returns a list of all motorcycles.</returns>
    /// <response code="200">List of motorcycles retrieved successfully</response>
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllMotorcycles()
    {
        var motorcycles = await _motorcycleService.GetAllMotorcyclesAsync();

        var motorcyclesDto = _mapper.Map<List<Motorcycle>, List<MotorcycleDTO>>(motorcycles.ToList());

        return Ok(motorcyclesDto);
    }
}
