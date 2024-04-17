using AutoMapper;
using CruiseControl.Application.DTO_s;
using CruiseControl.Core.Repositories;
using MediatR;

namespace CruiseControl.Application.Queries.GetAvailableCarsQuery
{
    public class GetAvailableCarsQueryHandler : IRequestHandler<GetAvailableCarsQuery, IEnumerable<CarDTO>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetAvailableCarsQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDTO>> Handle(GetAvailableCarsQuery query, CancellationToken cancellationToken)
        {
            var availableCars = await _carRepository.GetAvailableCars(query.PickupDate, query.ReturnDate);

            var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(availableCars);

            return carDTOs;
        }

    }
}
