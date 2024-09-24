using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using motcyApi.Application.DTOs;
using motcyApi.Application.DTOs.Validators;
using motcyApi.Application.Interfaces;
using motcyApi.Domain.Entities;

namespace motcyApi.Presentation.Controllers;

/// <summary>
/// Controller responsible for managing delivery personnel.
/// </summary>
[ApiController]
[Route("api/v1/deliverypersons")]
public class DeliveryPersonController : ControllerBase
{
    private readonly IDeliveryPersonService _deliveryPersonService;
    private readonly IStorageService _fileService;
    private readonly IMapper _mapper;

    public DeliveryPersonController(IDeliveryPersonService deliveryPersonService, IStorageService fileService, IMapper mapper)
    {
        _deliveryPersonService = deliveryPersonService;
        _fileService = fileService;
        _mapper = mapper;
    }

    /// <summary>
    /// Registers a new delivery person.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/deliverypersons
    ///     {
    ///        "name": "John Doe",
    ///        "cnpj": "12.345.678/0001-99",
    ///        "date_of_birth": "1990-01-01",
    ///        "license_number": "XYZ123456",
    ///        "license_type": "A",
    ///        "image_license": "base64EncodedImageString"
    ///        "email": "test@gmail.com"
    ///        "password": "password"
    ///        "password_confirmation": "password"
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
    public async Task<IActionResult> RegisterDeliveryPerson([FromBody] DeliveryPersonRegisterDTO deliveryPersonDto)
    {
        var validator = new DeliveryPersonRegisterDTOValidator();

        var results = validator.Validate(deliveryPersonDto);

        if (!results.IsValid)
        {
            var errors = results.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(new { errors });
        }

        var deliveryPerson = _mapper.Map<DeliveryPersonRegisterDTO, DeliveryPerson>(deliveryPersonDto);

        deliveryPerson.Id = Guid.NewGuid().ToString();

        var result = await _deliveryPersonService.RegisterDeliveryPersonAsync(deliveryPerson);

        string filePath = await _fileService.SaveFileFromBase64Async(deliveryPersonDto.ImageLicence, $"{deliveryPerson.Id}.png");

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
    public async Task<ActionResult<DeliveryPersonDTO>> GetDeliveryPersonById(string id)
    {
        var deliveryPerson = await _deliveryPersonService.GetDeliveryPersonByIdAsync(id);
        if (deliveryPerson == null)
        {
            return NotFound();
        }

        var imageBase64 = await _fileService.GetFileAsBase64Async($"{deliveryPerson.Id}.png");

        var deliveryPersonDto = _mapper.Map<DeliveryPerson, DeliveryPersonDTO>(deliveryPerson);

        deliveryPersonDto.ImageLicense = imageBase64;

        return Ok(deliveryPersonDto);
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
    public async Task<ActionResult<List<DeliveryPersonDTO>>> GetAllDeliveryPeople()
    {
        var deliveryPeople = await _deliveryPersonService.GetAllDeliveryPeopleAsync();

        var deliveryPeopleDto = new List<DeliveryPersonDTO>();

        if (deliveryPeople == null)
        {
            return Ok(deliveryPeopleDto);
        }

        foreach (var deliveryPerson in deliveryPeople)
        {
            var imageBase64 = await _fileService.GetFileAsBase64Async($"{deliveryPerson.Id}.png");

            var deliveryPersonDto = _mapper.Map<DeliveryPerson, DeliveryPersonDTO>(deliveryPerson);

            deliveryPersonDto.ImageLicense = imageBase64;

            deliveryPeopleDto.Add(deliveryPersonDto);
        }

        return Ok(deliveryPeopleDto);
    }
}
