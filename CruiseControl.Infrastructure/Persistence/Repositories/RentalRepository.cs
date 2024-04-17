using CruiseControl.Core.Entities;
using CruiseControl.Core.Exceptions;
using CruiseControl.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _appDbContext;

        public RentalRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Rental> GetRentalById(int id)
        {
            return await _appDbContext.Rentals.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Rental>> GetAllRentals()
        {
            return await _appDbContext.Rentals.ToListAsync();
        }

        public async Task AddRental(Rental rental)
        {
            _appDbContext.Rentals.Add(rental);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateRental(int id, Rental updatedRental)
        {
            var existingRental = await _appDbContext.Rentals.FindAsync(id);
            if (existingRental == null)
            {
                throw new NotFoundException($"Rental with id {id} not found");
            }

            existingRental.StartDate = updatedRental.StartDate;
            existingRental.EndDate = updatedRental.EndDate;
            existingRental.CarId = updatedRental.CarId;
            existingRental.CustomerId = updatedRental.CustomerId;
            existingRental.Price = updatedRental.Price;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    throw new NotFoundException($"Rental with id {id} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteRental(int id)
        {
            var rental = await _appDbContext.Rentals.FindAsync(id);
            if (rental == null)
            {
                throw new NotFoundException($"Rental with id {id} not found");
            }

            _appDbContext.Rentals.Remove(rental);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        private bool RentalExists(int id)
        {
            return _appDbContext.Rentals.Any(e => e.Id == id);
        }
    }
}
