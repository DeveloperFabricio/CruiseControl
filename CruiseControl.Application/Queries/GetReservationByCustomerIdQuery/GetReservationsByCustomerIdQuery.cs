using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Queries.GetReservationByCustomerIdQuery
{
    public class GetReservationsByCustomerIdQuery : IRequest<IEnumerable<ReservationDTO>>
    {
        public int CustomerId { get; set; }
    }
        
}
