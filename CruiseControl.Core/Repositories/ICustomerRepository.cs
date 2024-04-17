using CruiseControl.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruiseControl.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(int id);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task AddCustomer(Customer customer);
        Task UpdateCustomer(int id, Customer customer);
        Task DeleteCustomer(int id);
        Task SaveChangesAsync();
    }
}
