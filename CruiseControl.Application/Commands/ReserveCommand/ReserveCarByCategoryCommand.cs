using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Commands.ReserveCommand
{
    public class ReserveCarByCategoryCommand : IRequest<CarDTO>
    {
        public string Category { get; set; }
        public DateTime ReservationDate { get; set; }
    
    }
}
