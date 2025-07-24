using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Validators;

namespace GuineaPigApp.Server.UnitTests.Validators
{
    public class UserValidatorTests
    {
        [Fact]
        public void ThrowIfUserIsNull_WhenNull_ReturnsNotFoundException()
        {
            var userValidator = new UserValidator();

            User? user = null;

            Action action = () => userValidator.ThrowIfUserIsNull(user);

            var exception = Assert.Throws<NotFoundException>(action);

            Assert.Equal("Nie znaleziono takiego użytkownika.", exception.Message);
        }
    }
}
