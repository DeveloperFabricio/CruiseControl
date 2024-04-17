using AutoMapper;
using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Core.Entities;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommandHandler
{
    public class AddScheduleCommandHandler : IRequestHandler<AddScheduleCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AddScheduleCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddScheduleCommand command, CancellationToken cancellationToken)
        {
            var schedule = _mapper.Map<Schedule>(command.scheduleDTO);

            await _mediator.Send(new AddScheduleCommand { scheduleDTO = command.scheduleDTO });

            return Unit.Value;
        }

    }
}
