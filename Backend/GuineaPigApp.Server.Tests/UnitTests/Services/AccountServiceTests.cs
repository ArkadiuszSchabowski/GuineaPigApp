#nullable disable

using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using Microsoft.AspNetCore.Identity;
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
        [Fact]
        public void GenerateJWT_WhenValidLogin_ShouldGenerateJWTToken()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPasswordHasher = new Mock<IPasswordHasher<User>>();

            var loginUserDto = new LoginUserDto()
            {
                Email = "Test@gmail.com",
                Password = "Test123"
            };

            var user = new User()
            {
                Email = "Test@gmail.com",
                PasswordHash = "###",
                RoleId = 1
            };

            var testAuthenticationSettings = new AuthenticationSettings()
            {
                ExpireDays = 1,
                JwtKey = "TestKey TestKey TestKey TestKey TestKey",
                JwtIssuer = "Test Issuer"
            };

            var accountService = new AccountService(mockPasswordHasher.Object, null, testAuthenticationSettings, mockUserRepository.Object);

            mockPasswordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password)).Returns(PasswordVerificationResult.Success);

            mockUserRepository.Setup(x => x.GetUser(loginUserDto.Email)).Returns(user);

            var result = accountService.GenerateJWT(loginUserDto);

            Assert.IsType<string>(result);
        }

        [Fact]
        public void ChangePassword_WhenInvalidCurrentPassword_ShouldThrowBadRequestException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPasswordHasher = new Mock<IPasswordHasher<User>>();

            var accountService = new AccountService(mockPasswordHasher.Object, null, null, mockUserRepository.Object);

            var dto = new ChangePasswordDto()
            {
                Email = "test@gmail.com",
                CurrentPassword = "test1",
                NewPassword = "test2",
                RepeatNewPassword = "test2"
            };

            var user = new User();

            mockUserRepository.Setup(x => x.GetUser(dto.Email)).Returns(user);
            mockPasswordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, dto.CurrentPassword)).Returns(PasswordVerificationResult.Failed);

            Action action = () => accountService.ChangePassword(dto);

            var exception = Assert.Throws<BadRequestException>(action);
            Assert.Equal("Wprowadzono niepoprawne hasło!", exception.Message);     
        }
    }
}
