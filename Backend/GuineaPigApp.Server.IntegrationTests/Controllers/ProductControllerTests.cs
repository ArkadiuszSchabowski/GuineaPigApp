using FluentAssertions;
using GuineaPigApp.Server.Models;
using Newtonsoft.Json;
using System.Text;

namespace GuineaPigApp.Server.IntegrationTests.Controllers
{
    [Trait("Category", "Integration")]
    public class ProductControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private HttpClient _client;

        public ProductControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetBadProducts_WhenCalled_ShouldReturnStatusCodeOk()
        {
            var paginatorDto = new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10
            };
            var response = await _client.GetAsync($"/api/Product/bad?pageNumber={paginatorDto.PageNumber}&PageSize={paginatorDto.PageSize}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetGoodProducts_WhenCalled_ShouldReturnStatusCodeOk()
        {
            var paginatorDto = new PaginationDto()
            {
                PageNumber = 1,
                PageSize = 10
            };
            var response = await _client.GetAsync($"/api/Product/good?pageNumber={paginatorDto.PageNumber}&PageSize={paginatorDto.PageSize}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task AddProduct_WithCorrectModelButWithoutToken_ShouldReturnStatusCodeUnauthorized()
        {
            var model = new ProductDto()
            {
                Name = "Test Model",
                Description = "Test decription which have 15 letters",
                IsGoodProduct = true,
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
