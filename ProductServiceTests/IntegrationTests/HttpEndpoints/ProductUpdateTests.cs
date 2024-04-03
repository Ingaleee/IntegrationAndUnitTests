using Grpc.Core;
using Moq;
using OzonGrpc.Api.Dto;
using OzonGrpc.Api.Grpc;
using OzonGrpc.Api.Services;
using OzonGrpc.ProductService.Api;

namespace ProductServiceTests.IntegrationTests.HttpEndpoints
{
    public class ProductUpdateTests
    {
        [Fact]
        public async Task Update_Method_Should_Return_Success()
        {
            var productServiceMock = new Mock<IProductService>();
            _ = productServiceMock.Setup(x => x.Update(It.IsAny<UpdateProductDto>())).Returns(true);

            var grpcService = new ProductServiceGrpc(productServiceMock.Object);
            var request = new UpdateProductRequest
            {
                Id = 1,
                Name = "Updated Test Product",
                Weight = 20,
                Price = 200,
                Category = ProductCategory.General,
                WarehouseId = 1
            };
            ServerCallContext context = new Mock<ServerCallContext>().Object;

            UpdateProductResponse response = await grpcService.Update(request, context);

            Assert.NotNull(response);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task Update_Method_Should_Throw_Exception_On_Failure()
        {
            var productServiceMock = new Mock<IProductService>();
            _ = productServiceMock.Setup(x => x.Update(It.IsAny<UpdateProductDto>())).Throws(new Exception("Failed to update product"));

            var grpcService = new ProductServiceGrpc(productServiceMock.Object);
            var request = new UpdateProductRequest
            {
                Id = 1,
                Name = "Updated Test Product",
                Weight = 20,
                Price = 200,
                Category = OzonGrpc.ProductService.Api.ProductCategory.General,
                WarehouseId = 1
            };
            ServerCallContext context = new Mock<ServerCallContext>().Object;

            _ = await Assert.ThrowsAsync<Exception>(() => grpcService.Update(request, context));
        }
    }
}
