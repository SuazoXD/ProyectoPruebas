using System;
using Domain.Entities;
using Xunit;

namespace GrooveArchive.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_WithValidData_ShouldSucceed()
        {
            // Arrange
            string name = "Test Product";
            string description = "Test Description";
            decimal price = 10.0m;

            // Act
            var product = new Product(name, description, price);

            // Assert
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(price, product.Price);
        }

        [Fact]
        public void UpdatePrice_WithNegativeValue_ShouldThrowException()
        {
            // Arrange
            var product = new Product("Test", "Test", 10m);
            decimal newPrice = -5m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.UpdatePrice(newPrice));
        }
    }
}
