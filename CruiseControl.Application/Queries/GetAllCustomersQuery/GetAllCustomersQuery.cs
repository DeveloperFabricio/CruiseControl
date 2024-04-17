using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Queries.GetAllCustomersQuery
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDTO>>
    {
        
    }
}
