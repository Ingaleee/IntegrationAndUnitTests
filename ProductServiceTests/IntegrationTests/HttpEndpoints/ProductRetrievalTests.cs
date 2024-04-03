using Api.Filters;
using Grpc.Core;
using Moq;
using OzonGrpc.Api.Dto;
using OzonGrpc.Api.Grpc;
using OzonGrpc.Api.Services;
using OzonGrpc.ProductService.Api;

namespace ProductServiceTests.IntegrationTests.HttpEndpoints
{
    public class ProductRetrievalTests
    {
        [Fact]
        public async Task GetById_Method_Should_Throw_NotFoundException_When_Product_Not_Found()
        {
            var productServiceMock = new Mock<IProductService>();
            _ = productServiceMock.Setup(x => x.GetById(It.IsAny<ulong>())).Returns((GetProductDto)null);

            var grpcService = new ProductServiceGrpc(productServiceMock.Object);
            var request = new GetProductByIdRequest { Id = 1 };
            ServerCallContext context = new Mock<ServerCallContext>().Object;

            _ = await Assert.ThrowsAsync<RpcException>(() => grpcService.GetById(request, context));
        }
        [Fact]
        public async Task GetById_Method_Should_Return_Product()
        {
            var productServiceMock = new Mock<IProductService>();
            _ = productServiceMock.Setup(x => x.GetById(It.IsAny<ulong>())).Returns(new GetProductDto
            {
                Id = 1,
                Name = "Test Product",
                Weight = 10,
                Price = 100,
                Category = OzonGrpc.Domain.ProductCategory.General,
                CreatedUtc = DateTime.UtcNow,
                WarehouseId = 1
            });

            var grpcService = new ProductServiceGrpc(productServiceMock.Object);
            var request = new GetProductByIdRequest { Id = 1 };
            ServerCallContext context = new Mock<ServerCallContext>().Object;

            GetProductResponse response = await grpcService.GetById(request, context);

            Assert.NotNull(response);
            Assert.Equal("1", response.Id.ToString());
            Assert.Equal("Test Product", response.Name);
        }

        [Fact]
        public async Task List_Method_Should_Return_List_Of_Products()
        {
            var productServiceMock = new Mock<IProductService>();
            _ = productServiceMock.Setup(x => x.Get(It.IsAny<ListProductQuery>()))
                .Returns((ListProductQuery query) =>
                {
                    return new List<GetProductDto>
                    {
                    new() {
                        Id = 1,
                        Name = "Test Product 1",
                        Weight = 10,
                        Price = 100,
                        Category = OzonGrpc.Domain.ProductCategory.General,
                        CreatedUtc = DateTime.UtcNow,
                        WarehouseId = 1
                    },
                    new() {
                        Id = 2,
                        Name = "Test Product 2",
                        Weight = 20,
                        Price = 200,
                        Category = OzonGrpc.Domain.ProductCategory.General,
                        CreatedUtc = DateTime.UtcNow,
                        WarehouseId = 2
                    }
                    };
                });

            var grpcService = new ProductServiceGrpc(productServiceMock.Object);
            var request = new ListProductQuery();
            var requestGrpc = new ListProductQueryRequest
            {
                Category = ProductCategory.General.ToString(),
                WarehouseId = 1,
                Skip = 0,
                Take = 0
            };

            ServerCallContext context = new Mock<ServerCallContext>().Object;

            ListProductResponse response = await grpcService.List(requestGrpc, context);

            Assert.NotNull(response);
            Assert.Equal(2, response.Products.Count);
        }
    }
}
