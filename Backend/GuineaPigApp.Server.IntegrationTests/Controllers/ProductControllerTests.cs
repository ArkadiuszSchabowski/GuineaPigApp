using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GuineaPigApp.Server.IntegrationTests.Controllers
{
    public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _client;

        public ProductControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetBadProducts_WhenCalled_ShouldReturnStatusCodeOk()
        {
            var response = await _client.GetAsync("/api/product/bad");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetGoodProducts_WhenCalled_ShouldReturnStatusCodeOk()
        {

            var response = await _client.GetAsync("/api/product/good");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
