using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GrooveArchive.Tests
{
    public class ProductRepositoryTests
    {
        private ProjectDBContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ProjectDBContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new ProjectDBContext(options);
        }

        [Fact]
        public async Task CreateAndGetProductTest()
        {
            // Arrange
            var dbName = "CreateAndGetProductTestDb";
            using var context = GetInMemoryDbContext(dbName);
            var repository = new ProductRepository(context);
            var product = new Product("Test Product", "Test Description", 100m);

            // Act
            await repository.CreateAsync(product);
            var retrieved = await repository.GetByIdAsync(product.Id);

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal("Test Product", retrieved.Name);
            Assert.Equal("Test Description", retrieved.Description);
            Assert.Equal(100m, retrieved.Price);
        }

        [Fact]
        public async Task UpdateProductTest()
        {
            // Arrange
            var dbName = "UpdateProductTestDb";
            using var context = GetInMemoryDbContext(dbName);
            var repository = new ProductRepository(context);
            var product = new Product("Test Product", "Test Description", 100m);
            await repository.CreateAsync(product);

            // Act
            product.UpdatePrice(150m);
            bool updateResult = await repository.UpdateAsync(product);
            var updatedProduct = await repository.GetByIdAsync(product.Id);

            // Assert
            Assert.True(updateResult);
            Assert.Equal(150m, updatedProduct.Price);
        }

        [Fact]
        public async Task DeleteProductTest()
        {
            // Arrange
            var dbName = "DeleteProductTestDb";
            using var context = GetInMemoryDbContext(dbName);
            var repository = new ProductRepository(context);
            var product = new Product("Test Product", "Test Description", 100m);
            await repository.CreateAsync(product);

            // Act
            bool deleteResult = await repository.DeleteAsync(product.Id);
            var retrievedAfterDelete = await repository.GetByIdAsync(product.Id);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(retrievedAfterDelete);
        }
    }
}
