using CruiseControl.Application.DTO_s;
using FluentValidation;

namespace CruiseControl.Application.Validations
{
    public class CarDTOValidator : AbstractValidator<CarDTO>
    {
        public CarDTOValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty().WithMessage("Brand is required");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Year is required")
                .GreaterThan(0).WithMessage("Year must be greater than 0");

            RuleFor(x => x.PlateNumber)
                .NotEmpty().WithMessage("Plate number is required")
                .Matches(@"^[A-Z0-9-]*$").WithMessage("Plate number must contain only uppercase letters, numbers, and hyphens");
        }
    }
}
