using CruiseControl.Core.Entities;
using CruiseControl.Infrastructure.Persistence;
using CruiseControl.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CruiseControl.UnitTests.RepositoriesTests
{
    public class CarRepositoryTests
    {
        [Fact]
        public async Task GetAll_ReturnsAllCars()
        {
            var mockDbContext = new Mock<AppDbContext>();
            var cars = new List<Car>
            {
                new Car { Id = 1, Model = "Nivus" },
                new Car { Id = 2, Model = "Gol" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Car>>();
            mockDbSet.As<IQueryable<Car>>().Setup(m => m.Provider).Returns(cars.Provider);
            mockDbSet.As<IQueryable<Car>>().Setup(m => m.Expression).Returns(cars.Expression);
            mockDbSet.As<IQueryable<Car>>().Setup(m => m.ElementType).Returns(cars.ElementType);
            mockDbSet.As<IQueryable<Car>>().Setup(m => m.GetEnumerator()).Returns(cars.GetEnumerator());

            mockDbContext.Setup(c => c.Cars).Returns(mockDbSet.Object);
            var repository = new CarRepository(mockDbContext.Object);

            var result = await repository.GetAllCars(); 

            Assert.Equal(2, result.Count());
        }
    }
}
