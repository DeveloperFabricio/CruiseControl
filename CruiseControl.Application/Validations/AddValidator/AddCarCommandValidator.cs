using CruiseControl.Application.Commands.AddCommand;
using FluentValidation;

namespace CruiseControl.Application.Validations.AddValidator
{
    public class AddCarCommandValidator : AbstractValidator<AddCarCommand>
    {
        public AddCarCommandValidator()
        {
            RuleFor(x => x.carDTO).NotNull().SetValidator(new CarDTOValidator());
        }
    
    }
}
