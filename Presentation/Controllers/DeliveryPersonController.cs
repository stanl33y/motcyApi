using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsible for managing delivery personnel.
/// </summary>
[ApiController]
[Route("api/v1/entregadores")]
public class DeliveryPersonController : ControllerBase
{
    private readonly IDeliveryPersonService _deliveryPersonService;

    public DeliveryPersonController(IDeliveryPersonService deliveryPersonService)
    {
        _deliveryPersonService = deliveryPersonService;
    }

    /// <summary>
    /// Registers a new delivery person.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/v1/entregadores
    ///     {
    ///        "identificador": "123",
    ///        "nome": "John Doe",
    ///        "cnpj": "12345678000199",
    ///        "data_nascimento": "1990-01-01",
    ///        "numero_cnh": "XYZ123456",
    ///        "tipo_cnh": "A",
    ///        "imagem_cnh": "base64EncodedImageString"
    ///     }
    /// 
    /// Registers a new delivery person with the provided details.
    /// </remarks>
    /// <param name="deliveryPersonDto">Object containing delivery person details.</param>
    /// <returns>Returns the created delivery person or an error message.</returns>
    /// <response code="201">Delivery person successfully registered</response>
    /// <response code="400">Error registering delivery person</response>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterDeliveryPerson([FromBody] DeliveryPersonDTO deliveryPersonDto)
    {
        var result = await _deliveryPersonService.RegisterDeliveryPersonAsync(new DeliveryPerson
        {
            Id = deliveryPersonDto.Identificador,
            Name = deliveryPersonDto.Nome,
            Cnpj = deliveryPersonDto.Cnpj,
            DateOfBirth = deliveryPersonDto.DataNascimento,
            LicenseNumber = deliveryPersonDto.NumeroCnh,
            LicenseType = deliveryPersonDto.TipoCnh,
            LicenseImage = deliveryPersonDto.ImagemCnh
        });

        if (result == null)
        {
            return BadRequest("Error registering delivery person.");
        }

        return CreatedAtAction(nameof(GetDeliveryPersonById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Retrieves a delivery person by their ID.
    /// </summary>
    /// <param name="id">The delivery person ID.</param>
    /// <returns>Returns the delivery person details or 404 if not found.</returns>
    /// <response code="200">Delivery person details retrieved successfully</response>
    /// <response code="404">Delivery person not found</response>
    [Authorize(Roles = "admin, deliveryperson")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeliveryPersonById(string id)
    {
        var deliveryPerson = await _deliveryPersonService.GetDeliveryPersonByIdAsync(id);
        if (deliveryPerson == null)
        {
            return NotFound();
        }

        return Ok(deliveryPerson);
    }

    /// <summary>
    /// Updates the license image (CNH) of a delivery person.
    /// </summary>
    /// <param name="id">The delivery person ID.</param>
    /// <param name="imageBase64">Base64 encoded image of the license (CNH).</param>
    /// <returns>Returns 204 on success or 404 if the delivery person is not found.</returns>
    /// <response code="204">License image updated successfully</response>
    /// <response code="404">Delivery person not found</response>
    [Authorize(Roles = "admin, deliveryperson")]
    [HttpPut("{id}/cnh")]
    public async Task<IActionResult> UpdateLicenseImage(string id, [FromBody] string imageBase64)
    {
        var updated = await _deliveryPersonService.UpdateLicenseImageAsync(id, imageBase64);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Retrieves all registered delivery people.
    /// </summary>
    /// <returns>Returns a list of all delivery people.</returns>
    /// <response code="200">List of delivery people retrieved successfully</response>
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllDeliveryPeople()
    {
        var deliveryPeople = await _deliveryPersonService.GetAllDeliveryPeopleAsync();
        return Ok(deliveryPeople);
    }
}
