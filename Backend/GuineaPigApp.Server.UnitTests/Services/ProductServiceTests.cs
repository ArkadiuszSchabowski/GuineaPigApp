﻿#nullable disable

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
        [Fact]
        public void AddProduct_WhenProductExist_ShouldThrowConflictException()
        {
            var mockRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockRepository.Object, null, null);

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

            mockRepository.Setup(x => x.EnsureProductDoesNotExist(productDto.Name)).Returns(product);

            Action action = () => productService.AddProduct(productDto);

            var exception = Assert.Throws<ConflictException>(action);

            Assert.Equal("Podany produkt istnieje już w bazie danych!", exception.Message);
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
                IsGoodProduct = true
            };

            var mockRepository = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepository.Object, null, null);

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
            var productService = new ProductService(null, null, null);

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
                { Id = 1, Name = "Bad Product 1", Description = "Bad Product Description 1", IsGoodProduct = false },
                new Product
                { Id = 2, Name = "Bad Product 2", Description = "Bad Product Description 2", IsGoodProduct = false }
            };

            var mockRepository = new Mock<IProductRepository>();
            var mockPaginator = new Mock<IPaginatorValidator>();

            var productService = new ProductService(mockRepository.Object, null, mockPaginator.Object);

            var paginationDto = new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10
            };

            mockPaginator.Setup(x => x.ValidatePagination(paginationDto));
            mockRepository.Setup(x => x.GetBadProducts(paginationDto)).Returns(products);

            var result = productService.GetBadProducts(paginationDto);

            Assert.Equal(result, products);
        }
        [Fact]
        public void GetGoodProducts_ShouldReturn_GoodProductsList()
        {
            var products = new List<Product>()
            {
                new Product
                { Id = 1, Name = "Good Product 1", Description = "Good Product Description 1", IsGoodProduct = true },
                new Product
                { Id = 2, Name = "Good Product 2", Description = "Good Product Description 2", IsGoodProduct = true }
            };

            var paginationDto = new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10
            };

            var mockRepository = new Mock<IProductRepository>();
            var mockPaginator = new Mock<IPaginatorValidator>();

            var productService = new ProductService(mockRepository.Object, null, mockPaginator.Object);

            mockPaginator.Setup(x => x.ValidatePagination(paginationDto));
            mockRepository.Setup(x => x.GetGoodProducts(paginationDto)).Returns(products);

            var result = productService.GetGoodProducts(paginationDto);

            Assert.Equal(result, products);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void RemoveProduct_WhenInvalidId_ShouldReturnBadRequestException(int id)
        {
            var productService = new ProductService(null, null, null);

            Action action = () => productService.RemoveProduct(id);
            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Wartość Id musi być większa od 0!", exception.Message);
        }
    }
}
