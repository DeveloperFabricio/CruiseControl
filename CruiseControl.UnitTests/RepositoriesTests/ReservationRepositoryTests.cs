using CruiseControl.Core.Entities;
using CruiseControl.Infrastructure.Persistence;
using CruiseControl.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CruiseControl.UnitTests.RepositoriesTests
{
    public class ReservationRepositoryTests
    {
        [Fact]
        public async Task GetReservationsByCustomerId_ReturnsReservationsByCustomerId()
        {
            int customerId = 1;
            var mockDbContext = new Mock<AppDbContext>();
            var reservations = new List<Reservation>
            {
                new Reservation { Id = 1, CustomerId = 1 },
                new Reservation { Id = 2, CustomerId = 1 },
                new Reservation { Id = 3, CustomerId = 2 }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Reservation>>();
            mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservations.Provider);
            mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservations.Expression);
            mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservations.ElementType);
            mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservations.GetEnumerator());

            mockDbContext.Setup(c => c.Reservations).Returns(mockDbSet.Object);
            var repository = new ReservationRepository(mockDbContext.Object);

            var result = await repository.GetReservationsByCustomerId(customerId); 

            Assert.Equal(2, result.Count());
            Assert.All(result, r => Assert.Equal(customerId, r.CustomerId));
        }
    }
}
