using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Queries.GetAvailableCarsQuery
{
    public class GetAvailableCarsQuery : IRequest<IEnumerable<CarDTO>>
    {
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
    
    }
}
