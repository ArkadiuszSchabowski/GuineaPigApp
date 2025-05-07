using FluentAssertions;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Validators;

namespace GuineaPigApp.Server.UnitTests.Validators
{
    public class ProductValidatorTests
    {
        [Fact]
        public void ValidateProduct_WithTooShortName_ShouldThrowBadRequestException()
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
        public void ValidateProduct_WithIncorrectLengthDescription_ShouldThrowBadRequestException(int descriptionLength, string expectedMessage)
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
