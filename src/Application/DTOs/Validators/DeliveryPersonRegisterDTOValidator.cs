using FluentValidation;

namespace motcyApi.Application.DTOs.Validators;

public class DeliveryPersonRegisterDTOValidator : AbstractValidator<DeliveryPersonRegisterDTO>
{
    public DeliveryPersonRegisterDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("CNPJ is required.")
            .Matches(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$")
            .WithMessage("CNPJ must be in the format XX.XXX.XXX/XXXX-XX.");

        RuleFor(x => x.DateOfBirth)
            .Must(BeAtLeast18YearsOld)
            .WithMessage("Date of birth must be at least 18 years ago.");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .WithMessage("License number is required.");

        RuleFor(x => x.LicenseType)
            .NotEmpty()
            .WithMessage("License type is required.");

        RuleFor(x => x.ImageLicence)
            .NotEmpty()
            .WithMessage("License image is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email address must be valid.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.PasswordConfirmation)
            .Equal(x => x.Password)
            .WithMessage("Password confirmation must match password.");
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        var age = DateTime.Now.Year - dateOfBirth.Year;
        if (DateTime.Now.Month < dateOfBirth.Month || (DateTime.Now.Month == dateOfBirth.Month && DateTime.Now.Day < dateOfBirth.Day))
        {
            age--;
        }
        return age >= 18;
    }
}
