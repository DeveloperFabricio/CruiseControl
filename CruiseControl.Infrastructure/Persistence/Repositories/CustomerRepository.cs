using CruiseControl.Core.Entities;
using CruiseControl.Core.Exceptions;
using CruiseControl.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CruiseControl.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _appDbContext.Customers.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _appDbContext.Customers.ToListAsync();
        }

        public async Task AddCustomer(Customer customer)
        {
            _appDbContext.Customers.Add(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomer(int id, Customer updatedCustomer)
        {
            var existingCustomer = await _appDbContext.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                throw new NotFoundException($"Customer with id {id} not found");
            }

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Address = updatedCustomer.Address;
            existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
            existingCustomer.Email = updatedCustomer.Email;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    throw new NotFoundException($"Customer with id {id} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _appDbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with id {id} not found");
            }

            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        private bool CustomerExists(int id)
        {
            return _appDbContext.Customers.Any(e => e.Id == id);
        }
    }
}
