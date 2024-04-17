using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Application.Commands.DeleteCommand;
using CruiseControl.Application.Commands.ReserveCommand;
using CruiseControl.Application.DTO_s;
using CruiseControl.Application.Queries.GetAllCarsQuery;
using CruiseControl.Application.Queries.GetAvailableCarsByCategoryQuery;
using CruiseControl.Application.Queries.GetAvailableCarsQuery;
using CruiseControl.Application.Queries.GetCarByIdQuery;
using CruiseControl.Application.Queries.GetReservationsByCarIdQuery;
using CruiseControl.Core.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruiseControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;
       
        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{add-car}")]
        [Authorize]
        public async Task<IActionResult> AddCar(CarDTO carDTO)
        {
            try
            {
                await _mediator.Send(new AddCarCommand { carDTO = carDTO });
                return Ok("Car added successfully");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        [HttpPost("{client-reserve}")]
        [Authorize]
        public async Task<IActionResult> ReserveCarByCategory([FromBody] ReserveCarByCategoryCommand request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Category))
                {
                    return BadRequest("The car category is mandatory.");
                }

                var query = new GetAvailableCarsByCategoryQuery(request.Category);
                var availableCars = await _mediator.Send(query);

                if (availableCars == null || !availableCars.Any())
                {
                    return NotFound($"No cars available in category '{request.Category}' for the specified date.");
                }

                var selectedCar = availableCars.FirstOrDefault();
               
                return Ok(selectedCar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing reservation: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
        {
            
            var query = new GetAllCarsQuery();
            var cars = await _mediator.Send(query);

            
            if (cars == null || !cars.GetEnumerator().MoveNext())
            {
                return NoContent(); 
            }

            return Ok(cars);
        }

        [HttpGet("{available-cars}")] //  retorna os carros disponíveis para reserva em um determinado intervalo de datas.
        [Authorize]
        public async Task<IActionResult> GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        {
            var query = new GetAvailableCarsQuery { PickupDate = pickupDate, ReturnDate = returnDate };
            var availableCars = await _mediator.Send(query);
            return Ok(availableCars);
        }

        [HttpGet("{carId}")] //  usado para recuperar as reservas feitas para um carro específico.
        [Authorize]
        public async Task<IActionResult> GetReservationsByCarId(int carId)
        {
            var query = new GetReservationsByCarIdQuery { CarId = carId };
            var reservations = await _mediator.Send(query);
            return Ok(reservations);
        }

        [HttpGet("{id}")] // usado para recuperar informações específicas sobre um carro, como modelo, marca, ano de fabricação, etc.
        [Authorize]
        public async Task<IActionResult> GetCarById(int id)
        {
            var query = new GetCarByIdQuery { CarId = id };
            var car = await _mediator.Send(query);
            if (car != null)
            {
                return Ok(car);
            }
            return NotFound();
        }

        [HttpDelete("{carId}")] // exclusão de carros com base no ID do carro fornecido.
        [Authorize]
        public async Task<IActionResult> DeleteCar(int carId)
        {
            try
            {
                var command = new DeleteCarCommand { CarId = carId };

                await _mediator.Send(command);

                return Ok("Car deleted successfully");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
    }

}
