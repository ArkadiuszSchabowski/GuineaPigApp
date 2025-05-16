using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Validators;
using System;

namespace GuineaPigApp.Server.UnitTests.Validators
{
    public class GuineaPigValidatorTests
    {
        [Fact]
        public void ThrowIfUserGuineaPigExists_WhenExist_ReturnsConflictException()
        {
            var guineaPigValidator = new GuineaPigValidator();

            bool isGuineaPig = true;

            var action = () => guineaPigValidator.ThrowIfUserGuineaPigExists(isGuineaPig);

            var exception = Assert.Throws<ConflictException>(action);

            Assert.Equal("Posiadasz już świnkę morską o takim imieniu!", exception.Message);
        }
        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        [InlineData(49)]
        [InlineData(3001)]
        public void ThrowIfWeightIsNotCorrect_WithInvalidWeight_ReturnsBadRequestException(int weight)
        {
            var guineaPigValidator = new GuineaPigValidator();

            var action = () => guineaPigValidator.ThrowIfWeightIsNotCorrect(weight);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Waga świnki musi mieścić się w przedziale 50 do 3000 gram!", exception.Message);
        }
    }
}
