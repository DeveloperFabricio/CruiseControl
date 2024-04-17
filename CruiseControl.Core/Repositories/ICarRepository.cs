using CruiseControl.Core.Entities;

namespace CruiseControl.Core.Repositories
{
    public interface ICarRepository
    {
        Task<Car> GetCarById(int id);
        Task<IEnumerable<Car>> GetAllCars();
        Task<IEnumerable<Car>> GetAvailableCars(DateTime pickupDate, DateTime returnDate);
        Task<IEnumerable<Car>> GetAvailableCarsByCategoryAsync(string categoryType);
        Task AddCar(Car car);
        Task UpdateCar(int id, Car car);
        Task DeleteCar(int id);
        Task SaveChangesAsync();
    }
}
