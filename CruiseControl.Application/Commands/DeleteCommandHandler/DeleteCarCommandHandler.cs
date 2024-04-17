using AutoMapper;
using CruiseControl.Application.Commands.DeleteCommand;
using CruiseControl.Core.Entities;
using CruiseControl.Core.Exceptions;
using CruiseControl.Core.Repositories;
using MediatR;

namespace CruiseControl.Application.Commands.DeleteCommandHandler
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, Unit>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public DeleteCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request.CarId);

            int carId = car.Id;

            if (await _carRepository.GetCarById(carId) == null)
            {
                throw new NotFoundException($"Car with ID {carId} not found");
            }
            await _carRepository.DeleteCar(carId);

            return Unit.Value;
        }
    }
}
