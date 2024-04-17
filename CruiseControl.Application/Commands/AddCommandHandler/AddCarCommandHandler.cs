using AutoMapper;
using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Core.Entities;
using CruiseControl.Core.Repositories;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommandHandler
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, Unit>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public AddCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var carEntity = _mapper.Map<Car>(request.carDTO);

            await _carRepository.AddCar(carEntity);

            return Unit.Value;
        }
    }
}
