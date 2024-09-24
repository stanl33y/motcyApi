namespace motcyApi.Application.DTOs;

/// <summary>
/// Data Transfer Object used for delivery person registration and details.
/// </summary>
public class DeliveryPersonRegisterDTO
{
    /// <summary>
    /// Initializes a new instance of the DeliveryPersonDTO class.
    /// </summary>
    /// <param name="name">The full name of the delivery person.</param>
    /// <param name="cnpj">The CNPJ (Cadastro Nacional da Pessoa Jurídica) of the delivery person.</param>
    /// <param name="dateOfBirth">The date of birth of the delivery person.</param>
    /// <param name="licenseNumber">The license number (CNH) of the delivery person.</param>
    /// <param name="licenseType">The type of the license (CNH) of the delivery person.</param>
    /// <param name="imageLicence">The base64 encoded image of the license (CNH).</param>
    /// <param name="email">The email of the delivery person.</param>
    /// <param name="password">The password of the delivery person.</param>
    /// <param name="passwordConfirmation">The password confirmation of the delivery person.</param>
    public DeliveryPersonRegisterDTO(string name, string cnpj, DateTime dateOfBirth, string licenseNumber, string licenseType, string imageLicence, string email, string password, string passwordConfirmation)
    {
        Name = name;
        Cnpj = cnpj;
        DateOfBirth = dateOfBirth;
        LicenseNumber = licenseNumber;
        LicenseType = licenseType;
        ImageLicence = imageLicence;
        Email = email;
        Password = password;
        PasswordConfirmation = passwordConfirmation;
    }

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

    /// <summary>
    /// The email of the delivery person.
    /// </summary>
    /// <example>test@gmail.com</example>
    public string Email { get; set; }

    /// <summary>
    /// The password of the delivery person.
    /// </summary>
    /// <example>password</example>
    public string Password { get; set; }

    /// <summary>
    /// The password confirmation of the delivery person.
    /// </summary>
    /// <example>password</example>
    public string PasswordConfirmation { get; set; }
}
