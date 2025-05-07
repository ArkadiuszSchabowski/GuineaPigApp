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
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUserValidator> _mockUserValidator;
        private readonly Mock<IEmailValidator> _mockEmailValidator;
        private readonly Mock<IMapper> _mockMapper;
        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserValidator = new Mock<IUserValidator>();
            _mockEmailValidator = new Mock<IEmailValidator>();
            _mockMapper = new Mock<IMapper>();
        }
        [Fact]
        public void GetUser_WithInvalidEmail_ShouldThrowNotFoundException()
        {
            var userService = new UserService(_mockUserRepository.Object, _mockUserValidator.Object, _mockEmailValidator.Object, _mockMapper.Object);

            var email = "notfound@gmail.com";

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns((User?)null);

            Action action = () => userService.GetUser(email);

            var exception = Assert.Throws<NotFoundException>(action);

            Assert.Equal("Nie znaleziono użytkownika o podanym adresie e-mail!", exception.Message);
        }
        [Fact]
        public void UpdateUser_WithCorrectValues_ShouldMapOnce()
        {
            var userService = new UserService(_mockUserRepository.Object, _mockUserValidator.Object, _mockEmailValidator.Object, _mockMapper.Object);

            var email = "test@gmail.com";

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

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);
            _mockMapper.Setup(x => x.Map(updateUserDto, user)).Returns(user);
            _mockUserRepository.Setup(x => x.SaveChanges());

            userService.UpdateUser(email, updateUserDto);

            _mockMapper.Verify(x => x.Map(updateUserDto, user), Times.Once());
        }
        [Fact]
        public void GetUsers_WhenCalled_ShouldMapOnce()
        {
            var userService = new UserService(_mockUserRepository.Object, _mockUserValidator.Object, _mockEmailValidator.Object, _mockMapper.Object);

            var users = new List<User>();
            var usersDto = new List<GetUserDto>();

            _mockUserRepository.Setup(x => x.GetUsers()).Returns(users);
            _mockMapper.Setup(x => x.Map<List<GetUserDto>>(users)).Returns(usersDto);

            userService.GetUsers();

            _mockMapper.Verify(x =>x.Map<List<GetUserDto>>(users), Times.Once());
        }
    }
}
