#nullable disable

using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
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
        public void AddGuineaPig_WhenUserIsNull_ShouldThrowBadRequestException()
        {
            var guineaPigService = new GuineaPigService(_mockGuineaPigRepository.Object, _mockGuineaPigValidator.Object, _mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var guineaPigDto = new GuineaPigDto()
            {
                Name = "Test",
                Weight = 1000
            };

            var email = "correct@gmail.com";

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns((User)null);

            Action action = () => guineaPigService.AddGuineaPig(email, guineaPigDto);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Taki użytkownik nie istnieje!", exception.Message);
        }
        [Fact]
        public void AddGuineaPig_WhenPigExists_ShouldThrowConflictException()
        {
            var guineaPigService = new GuineaPigService(_mockGuineaPigRepository.Object, _mockGuineaPigValidator.Object, _mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var email = "correct@gmail.com";
            var user = new User()
            {
                Id = 1,
                Name = "Test"
            };
            var guineaPigDto = new GuineaPigDto()
            {
                Name = "Test",
                Weight = 1000
            };

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);
            _mockGuineaPigRepository.Setup(x => x.PigExists(user, guineaPigDto.Name)).Returns(true);

            Action action = () => guineaPigService.AddGuineaPig(email, guineaPigDto);

            var exception = Assert.Throws<ConflictException>(action);

            Assert.Equal("Posiadasz już taką świnkę morską o takim imieniu!", exception.Message);
        }
        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        [InlineData(49)]
        [InlineData(3001)]
        public void AddGuineaPig_WhenInvalidWeight_ShouldThrowBadRequestException(int weight)
        {
            var guineaPigService = new GuineaPigService(_mockGuineaPigRepository.Object, _mockGuineaPigValidator.Object, _mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var email = "correct@gmail.com";

            var user = new User()
            {
                Id = 1,
                Name = "Test"
            };

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);

            var guineaPigDto = new GuineaPigDto()
            {
                Name = "Test",
                Weight = weight
            };

            Action action = () => guineaPigService.AddGuineaPig(email, guineaPigDto);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Waga świnki musi mieścić się w przedziale 50 do 3000gram!", exception.Message);
        }
        [Fact]
        public void GetGuineaPigs_WhenCorrectEmail_ShouldInvokeMapOnce()
        {
            var guineaPigService = new GuineaPigService(_mockGuineaPigRepository.Object, _mockGuineaPigValidator.Object, _mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var email = "correct@gmail.com";

            var user = new User()
            {
                Id = 1,
                Name = "Test"
            };

            var listGuineaPigs = new List<GuineaPig>();

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);
            _mockGuineaPigRepository.Setup(x => x.GetGuineaPigs(user.Id)).Returns(listGuineaPigs);

            guineaPigService.GetGuineaPigs(email);

            _mockMapper.Verify(x => x.Map<List<GuineaPigDto>>(listGuineaPigs), Times.Once());
        }
        [Fact]
        public void RemoveGuineaPig_WhenCorrectParameters_ShouldInvokeGuineaPigRepositoryRemoveOnce()
        {
            var guineaPigService = new GuineaPigService(_mockGuineaPigRepository.Object, _mockGuineaPigValidator.Object, _mockUserRepository.Object, _mockUserValidator.Object, _mockMapper.Object);

            var email = "correct@gmail.com";

            var user = new User()
            {
                Id = 1,
                Name = "Test User"
            };

            var guineaPig = new GuineaPig()
            {
                Id = 1,
                Name = "Test Pig",
            };

            string guineaPigName = "Test Pig";

            _mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);
            _mockGuineaPigRepository.Setup(x => x.GetGuineaPig(user.Id, guineaPigName)).Returns(guineaPig);

            guineaPigService.RemoveGuineaPig(email, guineaPigName);

            _mockGuineaPigRepository.Verify(x => x.RemoveGuineaPig(guineaPig), Times.Once());
        }
    }
}
