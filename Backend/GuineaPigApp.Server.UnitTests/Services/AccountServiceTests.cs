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
    }
}
