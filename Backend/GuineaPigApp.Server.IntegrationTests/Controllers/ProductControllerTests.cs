using FluentAssertions;
using GuineaPigApp.Server.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

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
        [Fact]
        public async Task AddProduct_WithCorrectModelButWithoutToken_ShouldReturnStatusCodeUnauthorized()
        {
            var model = new ProductDto()
            {
                Name = "Test Model",
                Description = "Test decription which have 15 letters",
                isGoodProduct = true,
            };

            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/product", httpContent);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
        [Fact]
        public async Task RemoveProduct_WithCorrectIdButWithoutToken_ShouldReturnStatusCodeUnauthorized()
        {
            var id = 1;

            var response = await _client.DeleteAsync($"api/product/{id}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
