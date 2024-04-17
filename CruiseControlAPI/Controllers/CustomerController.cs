using CruiseControl.Application.Commands.AddCommand;
using CruiseControl.Application.DTO_s;
using CruiseControl.Application.Queries.GetAllCustomersQuery;
using CruiseControl.Application.Queries.GetReservationByCustomerIdQuery;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruiseControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCustomer(CustomerDTO customerDTO)
        {
            try
            {
                await _mediator.Send(new AddCustomerCommand { customerDTO = customerDTO });
                return Ok("Customer added successfully");
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            try
            {
                var query = new GetAllCustomersQuery();
                var customers = await _mediator.Send(query);

                if (customers == null || !customers.Any())
                {
                    return NoContent();
                }

                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        [HttpGet("{customerId}")] //  solicitação para obter reservas por ID do cliente.
        [Authorize]
        public async Task<IActionResult> GetReservationsByCustomerId(int customerId)
        {
            var query = new GetReservationsByCustomerIdQuery { CustomerId = customerId };
            var reservations = await _mediator.Send(query);
            return Ok(reservations);
        }
    }
}
