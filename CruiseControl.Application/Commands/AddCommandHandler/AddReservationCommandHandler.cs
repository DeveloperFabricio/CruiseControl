using AutoMapper;
using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Core.Entities;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommandHandler
{
    public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddReservationCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddReservationCommand command, CancellationToken cancellationToken)
        {
            
            var reservation = _mapper.Map<Reservation>(command.reservationDTO);

            await _mediator.Send(new AddReservationCommand { reservationDTO = command.reservationDTO });

            return Unit.Value;
        }

    }
}
