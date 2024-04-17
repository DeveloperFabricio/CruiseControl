using CruiseControl.Application.DTO_s;
using MediatR;

namespace CruiseControl.Application.Queries.GetAvailableCarsByCategoryQuery
{
    public class GetAvailableCarsByCategoryQuery : IRequest<IEnumerable<CarDTO>>
    {
        public string CategoryType { get; set; }
        public DateTime ReservationDate { get; set; }

        public GetAvailableCarsByCategoryQuery(string categoryType)
        {
            CategoryType = categoryType;
        }
    
    }
}
