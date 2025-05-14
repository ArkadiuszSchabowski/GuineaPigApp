using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Validators;

namespace GuineaPigApp.Server.UnitTests.Validators
{
    public class EmailValidatorTests
    {
        public void ValidateEmailFormat_WhenIsNull_ReturnsBadRequestException() 
        {
            var emailValidator = new EmailValidator();

            string? email = null;

            var action = () => emailValidator.ValidateEmailFormat(email!);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Adres e-mail nie może być pusty.", exception.Message);
        }

        [Theory]
        [InlineData("jamess@@gmail.com")]
        [InlineData("tom.com")]
        [InlineData("Tom@gmail.com ")]
        [InlineData("QXkoxkenqa")]
        [InlineData("   @james.com")]
        public void ValidateEmailFormat_WhenIsInvalid_ReturnsBadRequestException(string email)
        {
            var emailValidator = new EmailValidator();

            var action = () => emailValidator.ValidateEmailFormat(email);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Podany adres e-mail jest niepoprawny.", exception.Message);
        }
    }
}
