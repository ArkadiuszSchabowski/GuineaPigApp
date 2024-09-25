#nullable disable

using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Services;
using Moq;

namespace GuineaPigApp.Server.Tests.UnitTests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetProduct_WhenCorrectId_ShouldReturnProduct()
        {
            int correctId = 1;

            var product1 = new Product()
            {
                Id = 1,
                Name = "Test name 1",
                Description = "Test description 1",
                isGoodProduct = true
            };

            var mockRepository = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepository.Object);

            mockRepository.Setup(x => x.GetProduct(correctId)).Returns(product1);

            var result = productService.GetProduct(correctId);

            Assert.Equal(result, product1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void GetProduct_WhenInvalidId_ShouldThrowBadRequestException(int id)
        {
            var productService = new ProductService(null);

            Action action = () => productService.GetProduct(id);
            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Wartość Id musi być większa od 0!", exception.Message);
        }

        [Fact]
        public void GetBadProducts_ShouldReturn_BadProductsList()
        {
            var products = new List<Product>()
            {
                new Product
                { Id = 1, Name = "Bad Product 1", Description = "Bad Product Description 1", isGoodProduct = false },
                new Product
                { Id = 2, Name = "Bad Product 2", Description = "Bad Product Description 2", isGoodProduct = false }
            };

            var mockRepository = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepository.Object);

            mockRepository.Setup(x => x.GetBadProducts()).Returns(products);

            var result = productService.GetBadProducts();

            Assert.Equal(result, products);
        }
        [Fact]
        public void GetGoodProducts_ShouldReturn_GoodProductsList()
        {
            var products = new List<Product>()
            {
                new Product
                { Id = 1, Name = "Good Product 1", Description = "Good Product Description 1", isGoodProduct = true },
                new Product
                { Id = 2, Name = "Good Product 2", Description = "Good Product Description 2", isGoodProduct = true }
            };

            var mockRepository = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepository.Object);

            mockRepository.Setup(x => x.GetGoodProducts()).Returns(products);

            var result = productService.GetGoodProducts();

            Assert.Equal(result, products);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void RemoveProduct_WhenInvalidId_ShouldReturnBadRequestException(int id)
        {
            var productService = new ProductService(null);

            Action action = () => productService.RemoveProduct(id);
            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Wartość Id musi być większa od 0!", exception.Message);
        }
    }
}
