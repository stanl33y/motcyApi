using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsible for managing motorcycles.
/// </summary>
[ApiController]
[Route("api/v1/motos")]
public class MotorcycleController : ControllerBase
{
    private readonly IMotorcycleService _motorcycleService;

    public MotorcycleController(IMotorcycleService motorcycleService)
    {
        _motorcycleService = motorcycleService;
    }

    /// <summary>
    /// Creates a new motorcycle.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/v1/motos
    ///     {
    ///        "identificador": "123",
    ///        "ano": 2020,
    ///        "modelo": "Yamaha XTZ",
    ///        "placa": "XYZ-1234"
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
    public async Task<IActionResult> CreateMotorcycle([FromBody] MotorcycleDTO motorcycleDto)
    {
        var result = await _motorcycleService.AddMotorcycleAsync(new Motorcycle
        {
            Id = motorcycleDto.Identificador,
            Year = motorcycleDto.Ano,
            Model = motorcycleDto.Modelo,
            Plate = motorcycleDto.Placa
        });

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
    public async Task<IActionResult> GetMotorcycleById(string id)
    {
        var motorcycle = await _motorcycleService.GetMotorcycleByIdAsync(id);
        if (motorcycle == null)
        {
            return NotFound();
        }

        return Ok(motorcycle);
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
        var updatedMotorcycle = await _motorcycleService.UpdateMotorcyclePlateAsync(id, motorcycleDto.Placa);
        if (updatedMotorcycle == null)
        {
            return NotFound();
        }

        return Ok(updatedMotorcycle);
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
        return Ok(motorcycles);
    }
}
