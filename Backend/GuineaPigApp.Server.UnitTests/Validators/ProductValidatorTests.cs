using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Validators;

namespace GuineaPigApp.Server.UnitTests.Validators
{
    public class ProductValidatorTests
    {
        [Fact]
        public void ThrowIfProductExist_WhenProductExist_ShouldThrowConflictException()
        {
            var productValidator = new ProductValidator();

            var product = new Product()
            {
                Id = 1, Name = "Apple", IsGoodProduct = true
            };

            Action action = () => productValidator.ThrowIfProductExist(product);

            var exception = Assert.Throws<ConflictException>(action);

            Assert.Equal("Podany produkt istnieje już w bazie danych!", exception.Message);
        }

        [Fact]
        public void ThrowIfProductIsNotCorrect_WithTooShortName_ShouldThrowBadRequestException()
        {
            var productValidator = new ProductValidator();

            var productDto = new ProductDto()
            {
                Name = "X",
                Description = "Correct length description",
                IsGoodProduct = true
            };

            Action action = () => productValidator.ThrowIfProductIsNotCorrect(productDto);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Nazwa produktu nie może być krótsza niż 3 znaki!", exception.Message);
        }

        [Theory]
        [InlineData(0, "Opis produktu jest wymagany!")]
        [InlineData(14, "Opis produktu nie może być krótszy niż 15 znaków!")]
        [InlineData(1001, "Opis produktu nie może być dłuższy niż 1000 znaków!")]
        public void ThrowIfProductIsNotCorrect_WithIncorrectLengthDescription_ShouldThrowBadRequestException(int descriptionLength, string expectedMessage)
        {
            var productValidator = new ProductValidator();

            var productDto = new ProductDto()
            {
                Name = "Valid Name",
                Description = new string('A', descriptionLength),
                IsGoodProduct = true
            };

            Action action = () => productValidator.ThrowIfProductIsNotCorrect(productDto);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
