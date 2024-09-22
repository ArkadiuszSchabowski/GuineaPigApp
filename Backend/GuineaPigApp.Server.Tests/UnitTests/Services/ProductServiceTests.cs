using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Services;
using Moq;

namespace GuineaPigApp.Server.Tests.UnitTests.Services
{
    public class ProductServiceTests
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void GetProduct_WhenInvalidId_ShouldThrowBadRequestException(int id)
        {
            var mockRepository = new Mock<IProductRepository>();
            var mockValidator = new Mock<IProductValidator>();

            var productService = new ProductService(mockRepository.Object, mockValidator.Object);

            Action result = () => productService.GetProduct(id);

            Assert.Throws<BadRequestException>(result);
        }
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
            var mockValidator = new Mock<IProductValidator>();

            var productService = new ProductService(mockRepository.Object, mockValidator.Object);

            mockRepository.Setup(x => x.GetProduct(correctId)).Returns(product1);

            var result = productService.GetProduct(correctId);

            Assert.Equal(result, product1);
        }
    }
}
