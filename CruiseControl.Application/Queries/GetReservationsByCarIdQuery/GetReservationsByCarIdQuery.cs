using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Queries.GetReservationsByCarIdQuery
{
    public class GetReservationsByCarIdQuery : IRequest<IEnumerable<ReservationDTO>>
    {
        public int CarId { get; set; }
    }
}
