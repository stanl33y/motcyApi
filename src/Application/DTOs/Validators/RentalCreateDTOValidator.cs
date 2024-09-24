using FluentValidation;

namespace motcyApi.Application.DTOs.Validators;

public class RentalCreateDTOValidator : AbstractValidator<RentalCreateDTO>
{
    public RentalCreateDTOValidator()
    {
        RuleFor(r => r.DeliveryPersonId)
            .NotEmpty().WithMessage("Delivery Person ID is required.")
            .Length(1, 50).WithMessage("Delivery Person ID must be between 1 and 50 characters.");

        RuleFor(r => r.MotorcycleId)
            .NotEmpty().WithMessage("Motorcycle ID is required.")
            .Length(1, 50).WithMessage("Motorcycle ID must be between 1 and 50 characters.");

        RuleFor(r => r.StartDate)
            .NotEmpty().WithMessage("Start Date is required.")
            .GreaterThan(DateTime.Now).WithMessage("Start Date must be in the future.");

        RuleFor(r => r.RentalPlan)
            .InclusiveBetween(7, 50).WithMessage("Rental Plan must be between 7 and 50.");
    }
}
