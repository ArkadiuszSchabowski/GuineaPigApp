using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Validators;

namespace GuineaPigApp.Server.UnitTests.Validators
{
    public class NumberValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void ThrowIfIdIsNonPositive_WhenInvalidId_ShouldThrowBadRequestException(int id)
        {
            var numberValidator = new NumberValidator();

            Action action = () => numberValidator.ThrowIfIdIsNonPositive(id);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Wartość Id musi być większa od 0!", exception.Message);
        }
    }
}
