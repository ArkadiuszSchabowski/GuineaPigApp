using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using Moq;

namespace GuineaPigApp.Server.UnitTests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUser_WithInvalidEmail_ShouldThrowNotFoundException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockUserValidator = new Mock<IUserValidator>();

            var email = "notfound@gmail.com";

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockUserValidator.Object);

            mockUserValidator.Setup(x => x.ValidateEmailFormat(email));
            mockUserRepository.Setup(x => x.GetUser(email)).Returns((User?)null);

            Action action = () => userService.GetUser(email);

            var exception = Assert.Throws<NotFoundException>(action);

            Assert.Equal("Nie znaleziono użytkownika o podanym adresie e-mail!", exception.Message);
        }
        [Fact]
        public void UpdateUser_WithCorrectValues_ShouldMapOnce()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockUserValidator = new Mock<IUserValidator>();

            var email = "test@gmail.com";

            var userService = new UserService(mockUserRepository.Object, mockMapper.Object, mockUserValidator.Object);

            var user = new User
            {
                Email = "test@gmail.com",
                Name = "Arek",
                Surname = "Test",
                City = "Wrocław",
                RoleId = 1
            };

            var updateUserDto = new UpdateUserDto()
            {
                Name = "Arek",
                Surname = "Test",
                City = "Warszawa"
            };

            mockUserValidator.Setup(x => x.ValidateEmailFormat(email));
            mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);
            mockMapper.Setup(x => x.Map(updateUserDto, user)).Returns(user);
            mockUserRepository.Setup(x => x.SaveChanges());

            userService.UpdateUser(email, updateUserDto);

            mockMapper.Verify(x => x.Map(updateUserDto, user), Times.Once());
        }
    }
}
