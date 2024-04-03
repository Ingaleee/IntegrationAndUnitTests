using OzonGrpc.Domain;
using OzonGrpc.Infrastructure; 

namespace ProductServiceTests.UnitTests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            this._productRepository = new ProductRepository();
        }

        [Fact]
        public void Add_ShouldAddProduct()
        {
            // Arrange
            var product = new Product { Name = "Test Product", Price = 100 };

            // Act
            _ = this._productRepository.Add(product);

            // Assert
            Product? addedProduct = this._productRepository.GetById(product.Id);
            Assert.NotNull(addedProduct);
            Assert.Equal("Test Product", addedProduct.Name);
        }

        [Fact]
        public void Update_ExistingProduct_ShouldUpdateAndReturnTrue()
        {
            // Arrange
            var originalProduct = new Product { Name = "Original Product", Price = 100 };
            _ = this._productRepository.Add(originalProduct); 

            var updatedProduct = new Product { Id = originalProduct.Id, Name = "Updated Product", Price = 200 };

            // Act
            bool result = this._productRepository.Update(updatedProduct);

            // Assert
            Assert.True(result);
            Product? productFromRepo = this._productRepository.GetById(updatedProduct.Id);
            Assert.Equal("Updated Product", productFromRepo.Name);
            Assert.Equal(200, productFromRepo.Price);
        }

        [Fact]
        public void Update_NonExistingProduct_ShouldReturnFalse()
        {
            // Arrange
            var nonExistingProduct = new Product { Id = 999, Name = "Non-Existing", Price = 200 };

            // Act
            bool result = this._productRepository.Update(nonExistingProduct);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetById_ExistingProduct_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product { Name = "Test Product", Price = 100 };
            _ = this._productRepository.Add(product); 

            // Act
            Product? fetchedProduct = this._productRepository.GetById(product.Id);

            // Assert
            Assert.NotNull(fetchedProduct);
            Assert.Equal(product.Name, fetchedProduct.Name);
        }

        [Fact]
        public void GetById_NonExistingProduct_ShouldReturnNull()
        {
            // Act
            Product? result = this._productRepository.GetById(999); 

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_ShouldReturnAllProducts()
        {
            // Arrange
            var product1 = new Product { Name = "Product 1", Price = 100 };
            var product2 = new Product { Name = "Product 2", Price = 200 };
            _ = this._productRepository.Add(product1);
            _ = this._productRepository.Add(product2);

            // Act
            IEnumerable<Product> products = this._productRepository.Get(); 

            // Assert
            var productList = products.ToList();
            Assert.Equal(2, productList.Count);
            Assert.Contains(productList, p => p.Name == "Product 1");
            Assert.Contains(productList, p => p.Name == "Product 2");
        }

    }
}
