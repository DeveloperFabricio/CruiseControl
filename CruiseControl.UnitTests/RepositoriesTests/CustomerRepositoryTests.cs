using CruiseControl.Core.Entities;
using CruiseControl.Infrastructure.Persistence;
using CruiseControl.Infrastructure.Persistence.Repositories;
using Moq;

namespace CruiseControl.UnitTests.RepositoriesTests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public async Task GetCustomerById_ReturnsCustomerById()
        {
            int customerId = 1;
            var mockDbContext = new Mock<AppDbContext>();
            var customer = new Customer { Id = customerId, Name = "John Doe" };

            mockDbContext.Setup(c => c.Customers.FindAsync(customerId)).ReturnsAsync(customer);
            var repository = new CustomerRepository(mockDbContext.Object);

            var result = await repository.GetCustomerById(customerId); 

            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
            Assert.Equal(customer.Name, result.Name);
        }
    }
}
