using CruiseControl.Application.Commands.AddCommand;
using FluentValidation;

namespace CruiseControl.Application.Validations.AddValidator
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(x => x.customerDTO).NotNull().SetValidator(new CustomerDTOValidator());
        }
    
    }
}
