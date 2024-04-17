using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommand
{
    public class AddCustomerCommand : IRequest<Unit>
    {
        public CustomerDTO customerDTO { get; set; }
    }
}
