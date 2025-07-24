using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using GuineaPigApp_Server.Models.Add;
using GuineaPigApp_Server.Models.Get;
using Moq;

namespace GuineaPigApp.Server.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUserValidator> _mockUserValidator;
        private readonly Mock<IMapper> _mockMapper;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserValidator = new Mock<IUserValidator>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void GetUsers_WhenCalled_ShouldMapOnce()
        {
            var userService = new UserService(_mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var users = new List<User>();
            var usersDto = new List<GetUserDto>();

            _mockUserRepository.Setup(x => x.GetUsers()).Returns(users);

            userService.GetUsers();

            _mockMapper.Verify(x => x.Map<List<GetUserDto>>(users), Times.Once());
        }

        [Fact]
        public void UpdateUser_WithCorrectValues_ShouldMapOnce()
        {
            var userService = new UserService(_mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var user = new User
            {
                Email = "kowalski@gmail.com",
                Name = "Arek",
                Surname = "Kowalski",
                City = "Wrocław",
                RoleId = 1
            };

            var dto = new UpdateUserDto()
            {
                Name = "Arek",
                Surname = "Kowalski",
                City = "Warszawa"
            };

            _mockUserRepository.Setup(x => x.GetUser(user.Email)).Returns(user);

            userService.UpdateUser(user.Email, dto);

            _mockMapper.Verify(x => x.Map(dto, user), Times.Once());
        }
    }
}
