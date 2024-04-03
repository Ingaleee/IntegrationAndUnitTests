using Api.Filters;
using Moq;
using OzonGrpc.Api.Dto;
using OzonGrpc.Api.Services;
using OzonGrpc.Domain;
using OzonGrpc.Infrastructure;

namespace ProductServiceTests.UnitTests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            this._productRepositoryMock = new Mock<IProductRepository>();
            this._productService = new ProductService(this._productRepositoryMock.Object);
        }

        [Fact]
        public void Add_ValidProduct_ShouldReturnProductId()
        {
            var productDto = new CreateProductDto { Name = "New Product", Price = 100, Weight = 1, Category = ProductCategory.General, WarehouseId = 1 };
            _ = this._productRepositoryMock.Setup(repo => repo.Add(It.IsAny<Product>())).Returns(1);

            ulong result = this._productService.Add(productDto);

            Assert.Equal(1u, result);
        }

        [Fact]
        public void Update_ExistingProduct_ShouldReturnTrue()
        {
            ulong productId = 1UL; 
            var updateProductDto = new UpdateProductDto { Id = productId, Price = 200 };

            var existingProduct = new Product
            {
                Id = productId,
                Name = "Existing Product",
                Price = 100, 
            };

            _ = this._productRepositoryMock.Setup(repo => repo.GetById(productId)).Returns(existingProduct);
            _ = this._productRepositoryMock.Setup(repo => repo.Update(It.IsAny<Product>())).Returns(true);

            bool result = this._productService.Update(updateProductDto);

            Assert.True(result);
        }

        [Fact]
        public void Update_NonExistingProduct_ShouldReturnFalse()
        {
            var updateProductDto = new UpdateProductDto { Id = 999, Price = 200 };
            _ = this._productRepositoryMock.Setup(repo => repo.Update(It.IsAny<Product>())).Returns(false);

            bool result = this._productService.Update(updateProductDto);

            Assert.False(result);
        }

        [Fact]
        public void GetById_ExistingProduct_ShouldReturnProductDto()
        {
            // Arrange
            var expectedProduct = new Product { Id = 1, Name = "Test Product", Weight = 10, Price = 100, Category = ProductCategory.General, WarehouseId = 1 };
            _ = this._productRepositoryMock.Setup(repo => repo.GetById(1)).Returns(expectedProduct);

            // Act
            GetProductDto? result = this._productService.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Name, result.Name);
        }

        [Fact]
        public void GetById_NonExistingProduct_ShouldReturnNull()
        {
            // Arrange
            _ = this._productRepositoryMock.Setup(repo => repo.GetById(It.IsAny<ulong>())).Returns((Product)null);

            // Act
            GetProductDto? result = this._productService.GetById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_WithValidFilters_ShouldReturnFilteredProductsDto()
        {
            // Arrange
            var filter = new ListProductQuery { Category = ProductCategory.General };
            var expectedProducts = new List<Product>
            {
                new() { Id = 1, Name = "Product 1", Category = ProductCategory.General },
                new() { Id = 2, Name = "Product 2", Category = ProductCategory.General }
            };
            _ = this._productRepositoryMock.Setup(repo => repo.Get()).Returns(expectedProducts);

            // Act
            IEnumerable<GetProductDto> result = this._productService.Get(filter);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProducts.Count, result.Count());
        }
    }
}
