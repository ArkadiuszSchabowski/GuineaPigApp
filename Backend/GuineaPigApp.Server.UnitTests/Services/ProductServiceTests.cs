#nullable disable

using AutoMapper;
using FluentAssertions;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using Moq;

namespace GuineaPigApp.Server.UnitTests.Services
{
    [Trait("Category", "Unit")]
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IProductValidator> _mockProductValidator;
        private readonly Mock<IPaginatorValidator> _mockPaginatorValidator;
        private readonly Mock<INumberValidator> _mockNumberValidator;
        private readonly Mock<IMapper> _mockMapper;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductValidator = new Mock<IProductValidator>();
            _mockPaginatorValidator = new Mock<IPaginatorValidator>();
            _mockNumberValidator = new Mock<INumberValidator>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void Get_WhenCorrectId_ShouldReturnProductDto()
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            var product = new Product()
            {
                Id = 1, Name = "Apple", Description = "Apples are a good snack for guinea pigs.", IsGoodProduct = true
            };

            var productDto = new GetProductDto()
            {
                Name = "Apple", Description = "Apples are a good snack for guinea pigs.", IsGoodProduct = true
            };

            _mockProductRepository.Setup(x => x.GetProduct(product.Id)).Returns(product);

            _mockMapper.Setup(x => x.Map<GetProductDto>(product)).Returns(productDto);

            var result = productService.Get(product.Id);

            Assert.Equal(result, productDto);
        }

        [Fact]
        public void GetBadProductsResult_WithValidData_ReturnsBadProductsResult()
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            var products = new List<Product>()
            {
                new Product
                { 
                    Id = 1, Name = "Mushroom",
                    Description = "Mushrooms may contain toxins that are harmful to guinea pigs.",
                    IsGoodProduct = false },

                new Product
                { 
                    Id = 2, Name = "Garlic",
                    Description = "Garlic is completely banned for guinea pigs!",
                    IsGoodProduct = false }
            };

            var productsDto = new List<GetProductDto>()
            {
                new GetProductDto
                { 
                    Name = "Mushroom",
                    Description = "Mushrooms may contain toxins that are harmful to guinea pigs.",
                    IsGoodProduct = false },

                new GetProductDto
                { 
                    Name = "Garlic",
                    Description = "Garlic is completely banned for guinea pigs!",
                    IsGoodProduct = false }
            };

            var paginationDto = new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10
            };

            _mockProductRepository.Setup(x => x.CountBadProducts()).Returns(2);
            _mockProductRepository.Setup(x => x.GetBadProducts(paginationDto)).Returns(products);
            _mockMapper.Setup(x => x.Map<List<GetProductDto>>(products)).Returns(productsDto);

            var expectedResult = new ProductResultDto()
            {
                Products = productsDto,
                TotalCount = 2
            };

            var result = productService.GetBadProductsResult(paginationDto);

            result.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public void GetGoodProductsResult_WithValidData_ReturnsGoodProductsResult()
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            var productsDto = new List<GetProductDto>()
            {
                new GetProductDto
                { Name = "Apple", Description = "Apples are a good snack for guinea pigs.", IsGoodProduct = true },
                new GetProductDto
                { Name = "Cucumber", Description = "Cucumbers are low in calories and rich in water.", IsGoodProduct = true }
            };

            var products = new List<Product>()
            {
                new Product
                { Id =1, Name = "Apple", Description = "Apples are a good snack for guinea pigs.", IsGoodProduct = true },
                new Product
                { Id = 2, Name = "Cucumber", Description = "Cucumbers are low in calories and rich in water.", IsGoodProduct = true }
            };

            var paginationDto = new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10
            };

            _mockProductRepository.Setup(x => x.CountGoodProducts()).Returns(2);
            _mockProductRepository.Setup(x => x.GetGoodProducts(paginationDto)).Returns(products);
            _mockMapper.Setup(x => x.Map<List<GetProductDto>>(products)).Returns(productsDto);

            var expectedResult = new ProductResultDto()
            {
                Products = productsDto,
                TotalCount = 2
            };

            var result = productService.GetGoodProductsResult(paginationDto);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}