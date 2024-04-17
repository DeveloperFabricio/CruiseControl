using AutoMapper;
using CruiseControl.Application.DTO_s;
using CruiseControl.Core.Repositories;
using MediatR;

namespace CruiseControl.Application.Queries.GetAvailableCarsByCategoryQuery
{
    public class GetAvailableCarsByCategoryQueryHandler : IRequestHandler<GetAvailableCarsByCategoryQuery, IEnumerable<CarDTO>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetAvailableCarsByCategoryQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDTO>> Handle(GetAvailableCarsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var availableCars = await _carRepository.GetAvailableCarsByCategoryAsync(query.CategoryType);
            var carDTOs = _mapper.Map<IEnumerable<CarDTO>>(availableCars);
            return carDTOs;
        }
    

    }

}
