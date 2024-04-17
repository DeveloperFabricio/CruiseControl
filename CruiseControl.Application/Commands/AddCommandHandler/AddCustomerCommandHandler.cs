using AutoMapper;
using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Application.DTO_s;
using FluentValidation;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommandHandler
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Unit>
    {
        private readonly IValidator<AddCustomerCommand> _validator;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddCustomerCommandHandler(IMediator mediator, IValidator<AddCustomerCommand> validator, IMapper mapper)
        {
            _mediator = mediator;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var customer = _mapper.Map<CustomerDTO>(command.customerDTO);

            return Unit.Value;
        }
    
    }
}
