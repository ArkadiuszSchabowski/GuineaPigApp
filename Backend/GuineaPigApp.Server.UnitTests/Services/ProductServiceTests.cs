#nullable disable

using AutoMapper;
using FluentAssertions;
using GuineaPigApp;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
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
            
        }
        [Fact]
        public void AddProduct_WhenProductExist_ShouldThrowConflictException()
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            var productDto = new ProductDto()
            {
                Name = "Test ProductDto",
                IsGoodProduct = true
            };

            var product = new Product()
            {
                Name = "Test Product",
                IsGoodProduct = true
            };

            _mockProductRepository.Setup(x => x.GetProduct(productDto.Name)).Returns(product);

            Action action = () => productService.Add(productDto);

            var exception = Assert.Throws<ConflictException>(action);

            Assert.Equal("Podany produkt istnieje już w bazie danych!", exception.Message);
        }

        [Fact]
        public void GetProduct_WhenCorrectId_ShouldReturnProduct()
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            int correctId = 1;

            var product = new Product()
            {
                Id = 1,
                Name = "Test name 1",
                Description = "Test description 1",
                IsGoodProduct = true
            };

            var productDto = new GetProductDto()
            {
                Name = "Test name 1",
                Description = "Test description 1",
                IsGoodProduct = true
            };

            _mockProductRepository.Setup(x => x.GetProduct(correctId)).Returns(product);

            _mockMapper.Setup(x => x.Map<GetProductDto>(product)).Returns(productDto);

            var result = productService.Get(correctId);

            Assert.Equal(result, productDto);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void GetProduct_WhenInvalidId_ShouldThrowBadRequestException(int id)
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            Action action = () => productService.Get(id);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Wartość Id musi być większa od 0!", exception.Message);
        }

        [Fact]
        public void GetBadProductsResult_ShouldReturn_BadProductsResult()
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            var products = new List<Product>()
            {
                new Product
                { Id = 1, Name = "Bad Product 1", Description = "Bad Product Description 1", IsGoodProduct = false },
                new Product
                { Id = 2, Name = "Bad Product 2", Description = "Bad Product Description 2", IsGoodProduct = false }
            };

            var productsDto = new List<GetProductDto>()
            {
                new GetProductDto
                { Name = "Bad Product 1", Description = "Bad Product Description 1", IsGoodProduct = false },
                new GetProductDto
                { Name = "Bad Product 2", Description = "Bad Product Description 2", IsGoodProduct = false }
            };

            var paginationDto = new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10
            };

            _mockPaginatorValidator.Setup(x => x.ValidatePagination(paginationDto));
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
        public void GetGoodProductsResult_ShouldReturn_GoodProductsResult()
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            var productsDto = new List<GetProductDto>()
            {
                new GetProductDto
                { Name = "Good Product 1", Description = "Good Product Description 1", IsGoodProduct = true },
                new GetProductDto
                { Name = "Good Product 2", Description = "Good Product Description 2", IsGoodProduct = true }
            };

            var products = new List<Product>()
            {
                new Product
                { Id =1, Name = "Good Product 1", Description = "Good Product Description 1", IsGoodProduct = true },
                new Product
                { Id = 2, Name = "Good Product 2", Description = "Good Product Description 2", IsGoodProduct = true }
            };

            var paginationDto = new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10
            };
            var productResultDto = new ProductResultDto()
            {
                Products = productsDto,
                TotalCount = 2
            };

            _mockPaginatorValidator.Setup(x => x.ValidatePagination(paginationDto));
            _mockProductRepository.Setup(x => x.CountGoodProducts()).Returns(2);
            _mockProductRepository.Setup(x => x.GetGoodProducts(paginationDto)).Returns(products);
            _mockMapper.Setup(x => x.Map<List<GetProductDto>>(products)).Returns(productsDto);
            
            var result = productService.GetGoodProductsResult(paginationDto);

            result.Should().BeEquivalentTo(productResultDto);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void RemoveProduct_WhenInvalidId_ShouldReturnBadRequestException(int id)
        {
            var productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object, _mockPaginatorValidator.Object, _mockNumberValidator.Object, _mockMapper.Object);

            Action action = () => productService.RemoveProduct(id);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Wartość Id musi być większa od 0!", exception.Message);
        }
    }
}