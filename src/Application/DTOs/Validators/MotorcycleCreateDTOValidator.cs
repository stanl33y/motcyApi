using FluentValidation;

namespace motcyApi.Application.DTOs.Validators;

public class MotorcycleCreateDTOValidator : AbstractValidator<MotorcycleCreateDTO>
{
    public MotorcycleCreateDTOValidator()
    {
        RuleFor(x => x.Year)
            .GreaterThan(1900)
            .WithMessage("The manufacturing year must be greater than 1900");

        RuleFor(x => x.Model)
            .NotNull()
            .NotEmpty()
            .WithMessage("The motorcycle model is required")
            .MaximumLength(50)
            .WithMessage("The motorcycle model cannot exceed 50 characters");

        RuleFor(x => x.Plate)
            .NotNull()
            .NotEmpty()
            .WithMessage("The motorcycle plate is required")
            .Matches("^[A-Z]{3}-\\d{4}$")
            .WithMessage("The motorcycle plate must be in the format XXX-1234");
    }
}
