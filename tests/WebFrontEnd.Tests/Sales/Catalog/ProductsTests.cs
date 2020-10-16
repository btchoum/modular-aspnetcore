using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using WebFrontEnd.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace WebFrontEnd.Tests.Sales.Catalog
{
    public class ProductsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        // ReSharper disable once NotAccessedField.Local
        private readonly ITestOutputHelper _output;

        public ProductsTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _client = factory.CreateClient();
            _output = output;
        }
        
        [Fact]
        public async Task Create_Index_RoundTrip()
        {
            // Arrange
            var createPage = await HtmlDocument("/catalog/products/create");

            // Act
            var product = GivenProductInput();
            var formValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Command.Name", product.Name),
                new KeyValuePair<string, string>("Command.Description", product.Description)
            };

            var createForm = (IHtmlFormElement)createPage.QuerySelector("form");
            var submitButton = (IHtmlButtonElement)createPage.QuerySelector("button[type='submit']");
            var response = await _client.SendAsync(createForm, submitButton, formValues);
            var responseContent = await response.Content.ReadAsStringAsync();
            _output.WriteLine(responseContent);


            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(product.Name, responseContent);
            Assert.Contains(product.Description, responseContent);
            Assert.Contains(DateTime.Today.ToShortDateString(), responseContent);
        }

        private async Task<IHtmlDocument> HtmlDocument(string url)
        {
            var createPage = await _client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, createPage.StatusCode);

            return await HtmlHelpers.GetDocumentAsync(createPage);
        }

        private ProductInput GivenProductInput()
        {
            return new ProductInput
            {
                Name = $"Test Product Name - {Guid.NewGuid()}",
                Description = $"Test Product Description - {Guid.NewGuid()}"
            };
        }
    }

    class ProductInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
