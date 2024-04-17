using CruiseControl.Core.Entities;

namespace CruiseControl.Core.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsByCustomerId(int customerId);
        Task<IEnumerable<Reservation>> GetReservationsByCarId(int carId);
    }
}
