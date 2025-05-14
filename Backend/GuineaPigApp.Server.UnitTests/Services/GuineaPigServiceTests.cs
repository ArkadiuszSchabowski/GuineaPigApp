#nullable disable

using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using Moq;

namespace GuineaPigApp.Server.UnitTests.Services
{
    [Trait("Category", "Unit")]
    public class GuineaPigServiceTests
    {
        private readonly Mock<IGuineaPigRepository> _mockGuineaPigRepository;
        private readonly Mock<IGuineaPigValidator> _mockGuineaPigValidator;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IUserValidator> _mockUserValidator;
        private readonly Mock<IMapper> _mockMapper;
        public GuineaPigServiceTests()
        {
            _mockGuineaPigRepository = new Mock<IGuineaPigRepository>();
            _mockGuineaPigValidator = new Mock<IGuineaPigValidator>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserValidator = new Mock<IUserValidator>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void GetGuineaPigs_WhenCorrectEmail_ShouldInvokeMapOnce()
        {
            var guineaPigService = new GuineaPigService(_mockGuineaPigRepository.Object, _mockGuineaPigValidator.Object, _mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var user = new User()
            {
                Id = 1,
                Name = "Test",
                Email = "user@gmail.com"
            };

            var listGuineaPigs = new List<GuineaPig>();

            _mockUserRepository.Setup(x => x.GetUser(user.Email)).Returns(user);

            _mockGuineaPigRepository.Setup(x => x.GetGuineaPigs(user.Id)).Returns(listGuineaPigs);

            guineaPigService.GetGuineaPigs(user.Email);

            _mockMapper.Verify(x => x.Map<List<GuineaPigDto>>(listGuineaPigs), Times.Once());
        }

        [Fact]
        public void RemoveGuineaPig_WithCorrectData_ShouldInvokeGuineaPigRepositoryRemoveOnce()
        {
            var guineaPigService = new GuineaPigService(_mockGuineaPigRepository.Object, _mockGuineaPigValidator.Object, _mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var user = new User()
            {
                Id = 1, Name = "Tom", Email = "tom@gmail.com"
            };

            var guineaPig = new GuineaPig()
            {
                Id = 1, Name = "Pepa"
            };

            _mockUserRepository.Setup(x => x.GetUser(user.Email)).Returns(user);

            _mockGuineaPigRepository.Setup(x => x.GetGuineaPig(user.Id, guineaPig.Name)).Returns(guineaPig);

            guineaPigService.RemoveGuineaPig(user.Email, guineaPig.Name);

            _mockGuineaPigRepository.Verify(x => x.RemoveGuineaPig(guineaPig), Times.Once());
        }
    }
}
