using CruiseControl.Core.Entities;
using CruiseControl.Core.Exceptions;
using CruiseControl.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Repositories
{
    public class CarCategoryRepository : ICarCategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CarCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddCarCategory(CarCategory category)
        {
           await _appDbContext.CarCategorys.AddAsync(category);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteCarCategory(int id)
        {
            var category = await _appDbContext.CarCategorys.FindAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"CarCategory with id {id} not found");
               
            }
            _appDbContext.CarCategorys.Remove(category);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarCategory>> GetAllCarCategories()
        {
            return await _appDbContext.CarCategorys.ToListAsync();
        }

        public async Task<CarCategory> GetCarCategoryById(int id)
        {
            return await _appDbContext.CarCategorys.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateCarCategory(int id, CarCategory carCategory)
        {
            var existingCategory = await _appDbContext.CarCategorys.FindAsync(id);
            if (existingCategory == null)
            {
                throw new NotFoundException($"Car category with id {id} not found");
            }

            existingCategory.CategoryType = carCategory.CategoryType;

            _appDbContext.Entry(existingCategory).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarCategoryExists(id))
                {
                    throw new NotFoundException($"Car category with id {id} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CarCategoryExists(int id)
        {
            return _appDbContext.CarCategorys.Any(e => e.Id == id);
        }
    }
    
}
