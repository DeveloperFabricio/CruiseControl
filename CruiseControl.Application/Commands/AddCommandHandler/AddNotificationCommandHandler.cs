using AutoMapper;
using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Core.Entities;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommandHandler
{
    public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddNotificationCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddNotificationCommand command, CancellationToken cancellationToken)
        {
            
            var notification = _mapper.Map<Notification>(command.notificationDTO);

            await _mediator.Send(new AddNotificationCommand { notificationDTO = command.notificationDTO });

            return Unit.Value;
        }

    }
}
