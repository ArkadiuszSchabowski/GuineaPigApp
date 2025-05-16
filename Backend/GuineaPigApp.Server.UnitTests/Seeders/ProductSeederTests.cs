#nullable disable

using AutoMapper;
using GuineaPigApp.Server.Database;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Seeders;
using Moq;

namespace GuineaPigApp.Server.UnitTests.Seeders
{
    public class ProductSeederTests
    {
        private readonly Mock<IProductSeederRepository> _mockProductSeederRepository;
        private readonly Mock<IMapper> _mockMapper;

        public ProductSeederTests()
        {
            _mockProductSeederRepository = new Mock<IProductSeederRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void GetProducts_WithCorrectProductList_ShouldMapProductDtoToProductOnce()
        {
            var productSeeder = new ProductSeeder(_mockProductSeederRepository.Object, _mockMapper.Object, null);

            var productsDto = new List<ProductDto>{
                new ProductDto { Name = "Beetroot", Description = "Rich in vitamins and minerals", IsGoodProduct = true },
                new ProductDto { Name = "Tomato", Description = "Source of vitamin C and other nutrients", IsGoodProduct = true }
};

            var expectedProducts = new List<Product>{
                new Product { Name = "Beetroot", Description = "Rich in vitamins and minerals", IsGoodProduct = true },
                new Product { Name = "Tomato", Description = "Source of vitamin C and other nutrients", IsGoodProduct = true }};

            _mockMapper.Setup(x => x.Map<List<Product>>(productsDto)).Returns(expectedProducts);

            productSeeder.GetProducts(productsDto);

            _mockMapper.Verify(x => x.Map<List<Product>>(productsDto), Times.Once());
        }
    }
}