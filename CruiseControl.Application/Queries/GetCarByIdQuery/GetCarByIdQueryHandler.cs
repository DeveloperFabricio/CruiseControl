using AutoMapper;
using CruiseControl.Application.DTO_s;
using CruiseControl.Core.Repositories;
using MediatR;

namespace CruiseControl.Application.Queries.GetCarByIdQuery
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDTO>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetCarByIdQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<CarDTO> Handle(GetCarByIdQuery query, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetCarById(query.CarId);

            if (car == null)
            {
                return null;
            }

            var carDTO = _mapper.Map<CarDTO>(car);

            return carDTO;
        }
    }
}
