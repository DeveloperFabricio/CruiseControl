using CruiseControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseControl.Core.Repositories
{
    public interface ICarCategoryRepository
    {
        Task<CarCategory> GetCarCategoryById(int id);
        Task<IEnumerable<CarCategory>> GetAllCarCategories();
        Task AddCarCategory(CarCategory category);
        Task UpdateCarCategory(int id, CarCategory carCategory);
        Task DeleteCarCategory(int id);
        Task SaveChangesAsync();
    }
}
