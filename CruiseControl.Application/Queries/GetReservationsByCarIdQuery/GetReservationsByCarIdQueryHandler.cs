using AutoMapper;
using CruiseControl.Application.DTO_s;
using CruiseControl.Core.Repositories;
using MediatR;

namespace CruiseControl.Application.Queries.GetReservationsByCarIdQuery
{
    public class GetReservationsByCarIdQueryHandler : IRequestHandler<GetReservationsByCarIdQuery, IEnumerable<ReservationDTO>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationsByCarIdQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationDTO>> Handle(GetReservationsByCarIdQuery query, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetReservationsByCarId(query.CarId);

            var reservationDTOs = _mapper.Map<IEnumerable<ReservationDTO>>(reservations);

            return reservationDTOs;
        }

    }
}
