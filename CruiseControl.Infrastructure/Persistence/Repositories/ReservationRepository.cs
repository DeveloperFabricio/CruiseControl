using CruiseControl.Core.Entities;
using CruiseControl.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReservationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCustomerId(int customerId)
        {
            return await _appDbContext.Reservations
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByCarId(int carId)
        {
            return await _appDbContext.Reservations
                .Where(r => r.CarId == carId)
                .ToListAsync();
        }
    }
}

