using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Commands.AddCommand
{
    public class AddReservationCommand : IRequest<Unit>
    {
        public ReservationDTO reservationDTO { get; set; }
    }
}
