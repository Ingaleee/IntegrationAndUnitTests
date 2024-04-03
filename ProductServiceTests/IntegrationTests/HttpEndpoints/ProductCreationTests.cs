using Grpc.Core;
using Moq;
using OzonGrpc.Api.Dto;
using OzonGrpc.Api.Grpc;
using OzonGrpc.Api.Services;
using OzonGrpc.ProductService.Api;

namespace IntegrationTests.HttpEndpoints
{
    public class ProductCreationTests
    {
        [Fact]
        public async Task Add_Method_Should_Return_Id()
        {
            var productServiceMock = new Mock<IProductService>();
            _ = productServiceMock.Setup(x => x.Add(It.IsAny<CreateProductDto>())).Returns(1);

            var grpcService = new ProductServiceGrpc(productServiceMock.Object);
            var request = new AddProductRequest
            {
                Name = "Test Product",
                Weight = 10,
                Price = 100,
                Category = ProductCategory.General,
                WarehouseId = 1
            };
            ServerCallContext context = new Mock<ServerCallContext>().Object;

            AddProductResponse response = await grpcService.Add(request, context);

            Assert.NotNull(response);
            Assert.Equal("1", response.Id.ToString());
        }

        [Fact]
        public async Task Add_Method_Should_Throw_Exception_On_Failure()
        {
            var productServiceMock = new Mock<IProductService>();
            _ = productServiceMock.Setup(x => x.Add(It.IsAny<CreateProductDto>())).Throws(new Exception("Failed to add product"));

            var grpcService = new ProductServiceGrpc(productServiceMock.Object);
            var request = new AddProductRequest
            {
                Name = "Test Product",
                Weight = 10,
                Price = 100,
                Category = OzonGrpc.ProductService.Api.ProductCategory.General,
                WarehouseId = 1
            };
            ServerCallContext context = new Mock<ServerCallContext>().Object;

            _ = await Assert.ThrowsAsync<Exception>(() => grpcService.Add(request, context));
        }
    }
}
