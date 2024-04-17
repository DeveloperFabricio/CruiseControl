using CruiseControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseControl.Core.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> GetRentalById(int id);
        Task<IEnumerable<Rental>> GetAllRentals();
        Task AddRental(Rental rental);
        Task UpdateRental(int id, Rental rental);
        Task DeleteRental(int id);
        Task SaveChangesAsync();
    }
}
