using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Queries.GetAllCarsQuery
{
    public class GetAllCarsQuery : IRequest<IEnumerable<CarDTO>>
    {
    }
}
