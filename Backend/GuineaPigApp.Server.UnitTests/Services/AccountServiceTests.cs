#nullable disable

using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace GuineaPigApp.Server.UnitTests.Services
{
    [Trait("Category", "Unit")]
    public class AccountServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;
        private readonly Mock<IMapper> _mockMapper;
        public AccountServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _mockMapper = new Mock<IMapper>();
        }
        [Fact]
        public void RegisterUser_WhenPasswordsNotSame_ShouldThrowConflictException()
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var dto = new RegisterUserDto()
            {
                Email = "Test@gmail.com",
                Name = "Test",
                Surname = "Test",
                City = "Test",
                Password = "Password",
                RepeatPassword = "Wrong Password",
            };

            _mockUserRepository.Setup(x => x.GetUser(dto.Email)).Returns((User)null);

            Action action = () => accountService.RegisterUser(dto);

            var exception = Assert.Throws<ConflictException>(action);
            Assert.Equal("Wprowadzone hasła są róźne!", exception.Message);
        }

        [Fact]
        public void DeleteAccount_WhenUserIsNull_ShouldThrowBadRequestException()
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var email = "wrongEmail@gmail.com";

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns((User)null);

            Action action = () => accountService.DeleteAccount(email);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Taki użytkownik nie istnieje!", exception.Message);
        }

        [Fact]
        public void DeleteAccount_WithDefaultUser_ShouldThrowForbiddenException()
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var defaultUserEmail = "user@gmail.com";

            var defaultUser = new User()
            {
                Id = 1,
                Email = "user@gmail.com",
                Name = "Damian",
                Surname = "Kowalski",
                City = "Warszawa",
                RoleId = 1
            };

            _mockUserRepository.Setup(x => x.GetUser(defaultUserEmail)).Returns(defaultUser);

            Action action = () => accountService.DeleteAccount(defaultUserEmail);

            var exception = Assert.Throws<ForbiddenException>(action);

            Assert.Equal("Nie możesz usunąć domyślnego konta użytkownika!", exception.Message);
        }

        [Fact]
        public void GenerateJWT_WhenValidLogin_ShouldGenerateJWTToken()
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var loginUserDto = new LoginUserDto()
            {
                Email = "Test@gmail.com",
                Password = "Test123",
            };

            var user = new User()
            {
                Email = "Test@gmail.com",
                PasswordHash = "###",
                Role = new Role
                {
                    Id = 1,
                    Name = "Admin"
                }
            };

            var testAuthenticationSettings = new AuthenticationSettings()
            {
                ExpireDays = 1,
                JwtKey = "TestKey TestKey TestKey TestKey TestKey",
                JwtIssuer = "Test Issuer"
            };

            _mockPasswordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password)).Returns(PasswordVerificationResult.Success);

            _mockUserRepository.Setup(x => x.GetUser(loginUserDto.Email)).Returns(user);

            var result = accountService.GenerateJWT(loginUserDto);

            Assert.IsType<string>(result);
        }

        [Fact]
        public void ChangePassword_WhenInvalidCurrentPassword_ShouldThrowBadRequestException()
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var dto = new ChangePasswordDto()
            {
                Email = "test@gmail.com",
                CurrentPassword = "test1",
                NewPassword = "test2",
                RepeatNewPassword = "test2"
            };

            var user = new User();

            _mockUserRepository.Setup(x => x.GetUser(dto.Email)).Returns(user);
            _mockPasswordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, dto.CurrentPassword)).Returns(PasswordVerificationResult.Failed);

            Action action = () => accountService.ChangePassword(dto);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Wprowadzono niepoprawne hasło!", exception.Message);
        }
    }
}
