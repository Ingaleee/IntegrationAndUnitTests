using Newtonsoft.Json;
using OzonGrpc.Api.Dto;
using System.Net;
using System.Text;
using Xunit; 
using System.Threading.Tasks;
using System.Net.Http; 
using System;
using OzonGrpc.Domain;

namespace ProductServiceTests.IntegrationTests.GrpcEndpoints
{
    public class ProductServiceIntegrationPositiveTests
    {
        private readonly HttpClient _client;

        public ProductServiceIntegrationPositiveTests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5081")
            };
        }

        [Fact]
        public async Task CreateProduct_WithValidData_Returns_Created()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/v1/product/add");

            var content = new StringContent("{\r\n  \"name\": \"Example Product\",\r\n  \"weight\": 1.5,\r\n  \"price\": 100.0,\r\n  \"category\": \"General\",\r\n  \"warehouse_id\": 1\r\n}\r\n", null, "application/json");
            request.Content = content;
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProductById_WithValidId_Returns_OK()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/v1/product/add");

            var content = new StringContent("{\r\n  \"name\": \"Example Product\",\r\n  \"weight\": 1.5,\r\n  \"price\": 100.0,\r\n  \"category\": \"General\",\r\n  \"warehouse_id\": 1\r\n}\r\n", null, "application/json");
            request.Content = content;
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            request = new HttpRequestMessage(HttpMethod.Get, "/v1/product/1");
            response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task UpdateProductPrice_WithValidData_Returns_OK()
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "http://localhost:5081/v1/product/update");
            var content = new StringContent("{\r\n  \"id\": \"1\",\r\n  \"name\": \"Updated Product\",\r\n  \"price\": 150.0,\r\n  \"weight\": 1.0,\r\n  \"category\": \"HouseholdChemicals\",\r\n  \"warehouse_id\": 1\r\n}\r\n", null, "application/json");
            request.Content = content;
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task GetProductsList_Returns_OK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/v1/product/list");
            var response = await _client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
