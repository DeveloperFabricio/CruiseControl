using CruiseControl.Core.Entities;
using CruiseControl.Core.Enums;
using CruiseControl.Core.Exceptions;
using CruiseControl.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _appDbContext;

        public CarRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddCar(Car car)
        {
            await _appDbContext.Cars.AddAsync(car);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteCar(int id)
        {
            var cars = await _appDbContext.Cars.FindAsync(id);
            if (cars != null)
            {
                _appDbContext.Cars.Remove(cars);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await _appDbContext.Cars.ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAvailableCars(DateTime pickupDate, DateTime returnDate)
        {
            return await _appDbContext.Cars
           .Where(c => !_appDbContext.Reservations.Any(
               r => r.CarId == c.Id &&
               (pickupDate < r.ReturnDate && returnDate > r.PickupDate)))
           .ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAvailableCarsByCategoryAsync(string categoryType)
        {
            return await _appDbContext.Cars
            .Include(c => c.Category)
            .Where(c => c.Category.CategoryType == Enum.Parse<CarCategoryType>(categoryType) && c.IsAvailable) 
            .ToListAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _appDbContext.Cars.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateCar(int id, Car car)
        {
            if (id != car.Id)
            {
                throw new ArgumentException("Id mismatch");
            }

            _appDbContext.Entry(car).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    throw new NotFoundException($"Car with id {id} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CarExists(int id)
        {
            return _appDbContext.Cars.Any(e => e.Id == id);
        }

    }
}