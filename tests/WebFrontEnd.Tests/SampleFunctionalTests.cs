using System.Threading.Tasks;
using Xunit;

namespace WebFrontEnd.Tests
{
    public class SampleFunctionalTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public SampleFunctionalTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Catalog")]
        [InlineData("/Catalog/Products")]
        [InlineData("/Catalog/Products/Create")]
        [InlineData("/Index")]
        [InlineData("/Tickets")]
        public async Task Get_returns_success_and_correct_content_type(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
