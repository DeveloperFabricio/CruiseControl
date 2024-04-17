using AutoMapper;
using CruiseControl.Application.DTO_s;
using CruiseControl.Core.Repositories;
using MediatR;

namespace CruiseControl.Application.Queries.GetAllCarsQuery
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<CarDTO>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetAllCarsQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDTO>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carRepository.GetAllCars();

            var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(cars);

            return carDTOs;
        }

    }
}
