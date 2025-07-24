#nullable disable

using AutoMapper;
using FluentAssertions;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using GuineaPigApp_Server.Models.Add;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace GuineaPigApp.Server.UnitTests.Services
{
    [Trait("Category", "Unit")]
    public class AccountServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUserValidator> _mockUserValidator;
        private readonly Mock<ILoginValidator> _mockLoginValidator;
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;
        private readonly Mock<IMapper> _mockMapper;
        public AccountServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserValidator = new Mock<IUserValidator>();
            _mockLoginValidator = new Mock<ILoginValidator>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _mockMapper = new Mock<IMapper>();
        }
        [Fact]
        public void ChangePassword_WhenValidDto_ShouldInvokeUpdateUserOnce()
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockUserValidator.Object, _mockLoginValidator.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var dto = new ChangePasswordDto
            {
                Email = "tom@gmail.com",
                CurrentPassword = "house123",
                NewPassword = "city321",
                RepeatNewPassword = "city321",
            };

            var user = new User
            {
                Email = "tom@gmail.com",
                Name = "Tom",
                PasswordHash = "3k5H1"
            };

            var result = PasswordVerificationResult.Success;

            _mockUserRepository.Setup(x => x.GetUser(dto.Email)).Returns(user);
            _mockPasswordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, dto.CurrentPassword)).Returns(result);

            accountService.ChangePassword(dto);

            _mockUserRepository.Verify(x => x.UpdateUser(user), Times.Once);
        }

        [Fact]
        public void GenerateJWT_WhenValidLogin_ShouldGenerateJWTToken()
        {
            var testAuthenticationSettings = new AuthenticationSettings()
            {
                ExpireDays = 1, JwtKey = "TestKey TestKey TestKey TestKey TestKey", JwtIssuer = "Test Issuer"
            };

            var accountService = new AccountService(_mockUserRepository.Object, _mockUserValidator.Object, _mockLoginValidator.Object, _mockPasswordHasher.Object, _mockMapper.Object, testAuthenticationSettings);

            var loginUserDto = new LoginUserDto()
            {
                Email = "Test@gmail.com", Password = "Test123",
            };

            var user = new User()
            {
                Email = "Test@gmail.com", PasswordHash = "###", Role = new Role
                {
                    Id = 1, Name = "Admin"
                }
            };

            _mockUserRepository.Setup(x => x.GetUser(loginUserDto.Email)).Returns(user);
            _mockPasswordHasher.Setup(x => x.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password)).Returns(PasswordVerificationResult.Success);

            var result = accountService.GenerateJWT(loginUserDto);

            Assert.False(string.IsNullOrEmpty(result));
        }
        [Fact]
        public void RegisterUser_WhenValidDto_ShouldInvokeAddUserOnce()
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockUserValidator.Object, _mockLoginValidator.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var dto = new RegisterUserDto
            {
                Email = "tom@gmail.com",
                Name = "Tom",
                Password = "dog123",
                RepeatPassword = "dog123",
            };

            var registerUser = new User
            {
                Email = "tom@gmail.com",
                Name = "Tom",
                PasswordHash = "3k5H1"
            };

            registerUser.PasswordHash = "X123B";

            _mockUserRepository.Setup(x => x.GetUser(dto.Email)).Returns<User>(null);
            _mockMapper.Setup(x => x.Map<User>(dto)).Returns(registerUser);
            _mockPasswordHasher.Setup(x => x.HashPassword(registerUser, dto.Password)).Returns(registerUser.PasswordHash);

            accountService.RegisterUser(dto);

            _mockUserRepository.Verify(x => x.AddUser(registerUser), Times.Once);
        }
        [Fact]
        public void RegisterUser_WhenValidDto_ShouldSetRoleIdToOne()
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockUserValidator.Object, _mockLoginValidator.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var dto = new RegisterUserDto
            {
                Email = "tom@gmail.com",
                Name = "Tom",
                Password = "dog123",
                RepeatPassword = "dog123",
            };

            var registerUser = new User
            {
                Email = "tom@gmail.com",
                Name = "Tom",
                PasswordHash = "3k5H1"
            };

            registerUser.PasswordHash = "X123B";

            _mockUserRepository.Setup(x => x.GetUser(dto.Email)).Returns<User>(null);
            _mockMapper.Setup(x => x.Map<User>(dto)).Returns(registerUser);
            _mockPasswordHasher.Setup(x => x.HashPassword(registerUser, dto.Password)).Returns(registerUser.PasswordHash);

            accountService.RegisterUser(dto);

            registerUser.RoleId.Should().Be(1);
        }
        [Theory]
        [InlineData("tom@gmail.com")]
        [InlineData("TOM@interia.pl")]
        [InlineData("strong_as_bear@gmail.com")]
        public void RemoveAccount_WithEmail_ShouldInvokeRemoveUserOnce(string email)
        {
            var accountService = new AccountService(_mockUserRepository.Object, _mockUserValidator.Object, _mockLoginValidator.Object, _mockPasswordHasher.Object, _mockMapper.Object, null);

            var user = new User
            {
                Email = email
            };

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);

            accountService.RemoveAccount(email);

            _mockUserRepository.Verify(x => x.RemoveUser(user), Times.Once);
        }
    }
}
