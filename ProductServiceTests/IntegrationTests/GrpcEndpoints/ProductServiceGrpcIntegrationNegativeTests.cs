using Newtonsoft.Json;
using OzonGrpc.Api.Dto;
using System.Net;
using System.Text;

namespace ProductServiceTests.IntegrationTests.GrpcEndpoints
{
    public class ProductServiceIntegrationNegativeTests
    {
        private readonly HttpClient _client;

        public ProductServiceIntegrationNegativeTests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5081")
            };
        }

        [Fact]
        public async Task CreateProduct_WithInvalidData_Returns_BadRequest()
        {
            var product = new CreateProductDto
            {
                Name = "",
                Weight = -10,
                Price = 100,
                Category = (OzonGrpc.Domain.ProductCategory)OzonGrpc.ProductService.Api.ProductCategory.General,
                WarehouseId = 1
            };

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("/v1/product/add", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetProductById_WithInvalidId_Returns_NotFound()
        {
            HttpResponseMessage response = await _client.GetAsync("/v1/product/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProductPrice_WithInvalidData_Returns_BadRequest()
        {
            var product = new UpdateProductDto
            {
                Id = 1,
                Price = -200
            };

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync("/v1/product/update", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
