using AutoMapper;
using CruiseControl.Application.Commands.ReserveCommand;
using CruiseControl.Application.DTO_s;
using CruiseControl.Application.Queries.GetAvailableCarsByCategoryQuery;
using CruiseControl.Core.Exceptions;
using MediatR;

namespace CruiseControl.Application.Commands.ReserveCommandHandler
{
    public class ReserveCarByCategoryCommandHandler : IRequestHandler<ReserveCarByCategoryCommand, CarDTO>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReserveCarByCategoryCommandHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CarDTO> Handle(ReserveCarByCategoryCommand request, CancellationToken cancellationToken)
        {
            var query = new GetAvailableCarsByCategoryQuery(request.Category)
            {
                CategoryType = request.Category,
                ReservationDate = request.ReservationDate
            };
            var availableCars = await _mediator.Send(query);

            var selectedCar = availableCars.FirstOrDefault();
            if (selectedCar == null)
            {
                throw new NotFoundException($"No car available in the '{request.Category}' category for the specified date.");
            }

            var carDTO = _mapper.Map<CarDTO>(selectedCar);

            return carDTO;
        }
    }
}
