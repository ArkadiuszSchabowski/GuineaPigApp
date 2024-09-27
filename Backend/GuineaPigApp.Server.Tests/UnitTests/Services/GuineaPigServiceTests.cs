#nullable disable

using AutoMapper;
using GuineaPigApp.Server.Database.Entities;
using GuineaPigApp.Server.Exceptions;
using GuineaPigApp.Server.Interfaces;
using GuineaPigApp.Server.Models;
using GuineaPigApp.Server.Services;
using Moq;

namespace GuineaPigApp.Server.Tests.UnitTests.Services
{
    public class GuineaPigServiceTests
    {
        [Fact]
        public void AddGuineaPig_WhenUserIsNull_ShouldThrowBadRequestException()
        {
            var mockUserRepository = new Mock<IUserRepository>();

            var guineaPigService = new GuineaPigService(null, mockUserRepository.Object, null);

            var guineaPigDto = new GuineaPigDto()
            {
                Name = "Test",
                Weight = 1000
            };

            var email = "correct@gmail.com";

            mockUserRepository.Setup(x => x.GetUser(email)).Returns((User)null);

            Action action = () => guineaPigService.AddGuineaPig(email, guineaPigDto);

            var exception = Assert.Throws<BadRequestException>(action);

            Assert.Equal("Taki użytkownik nie istnieje!", exception.Message);
        }
        [Fact]
        public void AddGuineaPig_WhenPigExists_ShouldThrowConflictException()
        {
            var mockGuineaPigRepository = new Mock<IGuineaPigRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            var guineaPigService = new GuineaPigService(mockGuineaPigRepository.Object, mockUserRepository.Object, null);

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

            mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);
            mockGuineaPigRepository.Setup(x => x.PigExists(user, guineaPigDto.Name)).Returns(true);

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
            var mockUserRepository = new Mock<IUserRepository>();

            var guineaPigService = new GuineaPigService(null, mockUserRepository.Object, null);

            var email = "correct@gmail.com";

            var user = new User()
            {
                Id = 1,
                Name = "Test"
            };

            mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);

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
            var mockGuineaPigRepository = new Mock<IGuineaPigRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var guineaPigService = new GuineaPigService(mockGuineaPigRepository.Object, mockUserRepository.Object, mockMapper.Object);

            var email = "correct@gmail.com";

            var user = new User()
            {
                Id = 1,
                Name = "Test"
            };

            var listGuineaPigs = new List<GuineaPig>();

            mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);
            mockGuineaPigRepository.Setup(x => x.GetGuineaPigs(user.Id)).Returns(listGuineaPigs);

            guineaPigService.GetGuineaPigs(email);

            mockMapper.Verify(x => x.Map<List<GuineaPigDto>>(listGuineaPigs), Times.Once());
        }
        [Fact]
        public void RemoveGuineaPig_WhenCorrectParameters_ShouldInvokeGuineaPigRepositoryRemoveOnce()
        {
            var mockGuineaPigRepository = new Mock<IGuineaPigRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            var guineaPigService = new GuineaPigService(mockGuineaPigRepository.Object, mockUserRepository.Object, null);

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

            mockUserRepository.Setup(x => x.GetUser(email)).Returns(user);
            mockGuineaPigRepository.Setup(x => x.GetGuineaPig(user.Id, guineaPigName)).Returns(guineaPig);

            guineaPigService.RemoveGuineaPig(email, guineaPigName);

            mockGuineaPigRepository.Verify(x => x.RemoveGuineaPig(guineaPig), Times.Once());
        }
    }
}
