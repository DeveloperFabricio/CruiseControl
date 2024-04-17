using AutoMapper;
using CruiseControl.Application.DTO_s;
using CruiseControl.Core.Repositories;
using MediatR;

namespace CruiseControl.Application.Queries.GetReservationByCustomerIdQuery
{
    public class GetReservationsByCustomerIdQueryHandler : IRequestHandler<GetReservationsByCustomerIdQuery, IEnumerable<ReservationDTO>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationsByCustomerIdQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDTO>> Handle(GetReservationsByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetReservationsByCustomerId(query.CustomerId);

            var reservationDTOs = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);

            return reservationDTOs;
        }
    }
}

