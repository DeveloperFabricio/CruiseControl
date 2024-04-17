using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommand
{
    public class AddCarCommand : IRequest<Unit>
    {
        public CarDTO carDTO {  get; set; }
    }
}
