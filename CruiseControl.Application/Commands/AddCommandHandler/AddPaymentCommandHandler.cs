using AutoMapper;
using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Core.Entities;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommandHandler
{
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddPaymentCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddPaymentCommand command, CancellationToken cancellationToken)
        {
            
            var payment = _mapper.Map<Payment>(command.paymentDTO);

            await _mediator.Send(new AddPaymentCommand { paymentDTO = command.paymentDTO });

            return Unit.Value;
        }


    }
}
