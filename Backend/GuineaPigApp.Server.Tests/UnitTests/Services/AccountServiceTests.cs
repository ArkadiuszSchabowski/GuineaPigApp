#nullable disable

using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using Moq;

namespace GuineaPigApp.Server.Tests.UnitTests.Services
{
    public class AccountServiceTests
    {
        [Fact]
        public void RegisterUser_WhenPasswordsNotSame_ShouldThrowConflictException()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            var accountService = new AccountService(null, null, null, mockUserRepository.Object);

            var dto = new RegisterUserDto()
            {
                Email = "Test@gmail.com",
                Name = "Test",
                Surname = "Test",
                City = "Test",
                Password = "Password",
                RepeatPassword = "Wrong Password",
            };

            mockUserRepository.Setup(x => x.GetUser(dto.Email)).Returns((User)null);

            Action action = () => accountService.RegisterUser(dto);

            var exception = Assert.Throws<ConflictException>(action);
            Assert.Equal("Wprowadzone hasła są róźne", exception.Message);
        }

        [Fact]
        public void DeleteAccount_WhenUserIsNull_ShouldThrowBadRequestException()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            var accountService = new AccountService(null, null, null, mockUserRepository.Object);

            var email = "wrongEmail@gmail.com";

            mockUserRepository.Setup(x => x.GetUser(email)).Returns((User)null);

            Action action = () => accountService.DeleteAccount(email);

            var exception = Assert.Throws<BadRequestException>(action);
            Assert.Equal("Taki użytkownik nie istnieje!", exception.Message);
        }
    }
}
