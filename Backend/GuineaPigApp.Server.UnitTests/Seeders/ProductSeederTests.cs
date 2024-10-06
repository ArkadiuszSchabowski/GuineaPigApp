#nullable disable

using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Seeders;
using Moq;
using FluentAssertions;

namespace GuineaPigApp.Server.UnitTests.Seeders
{
    public class ProductSeederTests
    {
        [Fact]
        public void GetProducts_WithCorrectList_ShouldMapProductDtoToProduct()
        {
            var mockMapper = new Mock<IMapper>();
            var productSeeder = new ProductSeeder(null, mockMapper.Object);

            List<ProductDto> productsDto = new List<ProductDto>()
            {
                new ProductDto()
                {
                    Name = "Test",
                    Description = "Product which have over than 15 letters description",
                    IsGoodProduct = true,
                },
                new ProductDto()
                {
                    Name = "Test2",
                    Description = "Product which have over than 15 letters description",
                    IsGoodProduct = false,
                }
            };

            var expectedProducts = new List<Product>()
            {
                new Product()
                {
                    Name = "Test",
                    Description = "Product which have over than 15 letters description",
                    IsGoodProduct = true,
                },
                new Product()
                {
                    Name = "Test2",
                    Description = "Product which have over than 15 letters description",
                    IsGoodProduct = false,
                },
            };

            mockMapper.Setup(x => x.Map<List<Product>>(productsDto)).Returns(expectedProducts);

            productSeeder.GetProducts(productsDto);

            mockMapper.Verify(x => x.Map<List<Product>>(productsDto), Times.Once());
        }
    }
}