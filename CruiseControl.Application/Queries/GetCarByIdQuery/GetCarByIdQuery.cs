using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Queries.GetCarByIdQuery
{
    public class GetCarByIdQuery : IRequest<CarDTO>
    {
        public int CarId { get; set; }
        public string Category { get; set; }
    }
}
