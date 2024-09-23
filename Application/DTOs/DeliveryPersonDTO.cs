/// <summary>
/// Data Transfer Object used for delivery person registration and details.
/// </summary>
public class DeliveryPersonDTO
{
    /// <summary>
    /// The unique identifier of the delivery person.
    /// </summary>
    /// <example>123</example>
    public string Id { get; set; }

    /// <summary>
    /// The full name of the delivery person.
    /// </summary>
    /// <example>John Doe</example>
    public string Name { get; set; }

    /// <summary>
    /// The CNPJ (Cadastro Nacional da Pessoa Jurídica) of the delivery person.
    /// </summary>
    /// <example>12345678000199</example>
    public string Cnpj { get; set; }

    /// <summary>
    /// The date of birth of the delivery person.
    /// </summary>
    /// <example>1990-01-01</example>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// The license number (CNH) of the delivery person.
    /// </summary>
    /// <example>XYZ123456</example>
    public string LicenseNumber { get; set; }

    /// <summary>
    /// The type of the license (CNH) of the delivery person.
    /// </summary>
    /// <example>A</example>
    public string LicenseType { get; set; }

    /// <summary>
    /// The base64 encoded image of the license (CNH).
    /// </summary>
    /// <example>base64EncodedImageString</example>
    public string ImageLicence { get; set; }
}
