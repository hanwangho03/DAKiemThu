//using AutoMapper;
//using CloudinaryDotNet.Actions;
//using CloudinaryDotNet;
//using Moq;
//using WebDoDienTu.Models;
//using WebDoDienTu.Repository;
//using WebDoDienTu.Service;
//using MoblieShop.Tests.MockData;

//namespace MoblieShop.Tests.UnitTests.Services
//{
//    public class ProductServiceTests
//    {
//        private readonly Mock<IProductRepository> _productRepositoryMock;
//        private readonly Mock<ICloudinary> _cloudinaryMock;
//        private readonly Mock<IMapper> _mapperMock;
//        private readonly ProductService _productService;

//        public ProductServiceTests()
//        {
//            _productRepositoryMock = new Mock<IProductRepository>();
//            _cloudinaryMock = new Mock<ICloudinary>();
//            _mapperMock = new Mock<IMapper>();

//            _productService = new ProductService(
//                _productRepositoryMock.Object,
//                _cloudinaryMock.Object,
//                _mapperMock.Object
//            );
//        }

//        // ✅ 1. GetAllProductsAsync - Lấy danh sách sản phẩm
//        [Fact]
//        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
//        {
//            // Arrange
//            var products = ProductMockData.GetSampleProducts();
//            _productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

//            // Act
//            var result = await _productService.GetAllProductsAsync();

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(2, result.Count());
//            Assert.Equal("Laptop", result.First().ProductName);
//        }

//        [Fact]
//        public async Task GetAllProductsAsync_ShouldReturnEmptyList_WhenNoProducts()
//        {
//            // Arrange
//            _productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());

//            // Act
//            var result = await _productService.GetAllProductsAsync();

//            // Assert
//            Assert.Empty(result);
//        }

//        // ✅ 2. GetProductByIdAsync - Lấy sản phẩm theo ID
//        [Fact]
//        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
//        {
//            // Arrange
//            var product = ProductMockData.GetSampleProduct();
//            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

//            // Act
//            var result = await _productService.GetProductByIdAsync(1);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal("Laptop", result.ProductName);
//        }

//        [Fact]
//        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
//        {
//            // Arrange
//            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

//            // Act
//            var result = await _productService.GetProductByIdAsync(1);

//            // Assert
//            Assert.Null(result);
//        }

//        // ✅ 3. CreateProductAsync - Tạo sản phẩm
//        //[Fact]
//        //public async Task CreateProductAsync_ShouldCallRepositoryAddAsync()
//        //{
//        //    // Arrange
//        //    var productViewModel = ProductMockData.GetSampleProductCreateViewModel();
//        //    var product = new Product { ProductName = "Laptop" };

//        //    _mapperMock.Setup(m => m.Map<Product>(productViewModel)).Returns(product);

//        //    _cloudinaryMock.Setup(c => c.UploadAsync(It.IsAny<ImageUploadParams>()))
//        //        .ReturnsAsync(new ImageUploadResult { SecureUrl = new System.Uri("https://fakeurl.com/image.jpg") });

//        //    _cloudinaryMock.Setup(c => c.UploadLargeAsync<VideoUploadResult>(It.IsAny<VideoUploadParams>(), It.IsAny<int>(), It.IsAny<int>()))
//        //        .ReturnsAsync(new VideoUploadResult { SecureUrl = new System.Uri("https://fakeurl.com/video.mp4") });

//        //    _productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
//        //        .Returns(Task.CompletedTask);

//        //    // Act
//        //    await _productService.CreateProductAsync(productViewModel);

//        //    // Assert
//        //    _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
//        //}

//        //[Fact]
//        //public async Task CreateProductAsync_ShouldThrowException_WhenUploadFails()
//        //{
//        //    // Arrange
//        //    var productViewModel = ProductMockData.GetSampleProductCreateViewModel();

//        //    _cloudinaryMock.Setup(c => c.UploadAsync(It.IsAny<ImageUploadParams>()))
//        //        .ReturnsAsync(new ImageUploadResult { SecureUrl = null });

//        //    // Act & Assert
//        //    await Assert.ThrowsAsync<Exception>(() => _productService.CreateProductAsync(productViewModel));
//        //}

//        // ✅ 4. UpdateProductAsync - Cập nhật sản phẩm
//        [Fact]
//        public async Task UpdateProductAsync_ShouldThrowException_WhenProductDoesNotExist()
//        {
//            // Arrange
//            var updateViewModel = ProductMockData.GetSampleProductUpdateViewModel();
//            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(updateViewModel.ProductId))
//                .ReturnsAsync((Product)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<Exception>(() => _productService.UpdateProductAsync(updateViewModel));
//        }

//        [Fact]
//        public async Task UpdateProductAsync_ShouldUpdateProduct_WhenValid()
//        {
//            // Arrange
//            var product = ProductMockData.GetSampleProduct();
//            var updateViewModel = ProductMockData.GetSampleProductUpdateViewModel();

//            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(updateViewModel.ProductId))
//                .ReturnsAsync(product);

//            _mapperMock.Setup(m => m.Map(updateViewModel, product));

//            _productRepositoryMock.Setup(repo => repo.UpdateAsync(product))
//                .Returns(Task.CompletedTask);

//            // Act
//            await _productService.UpdateProductAsync(updateViewModel);

//            // Assert
//            _productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
//        }

//        // ✅ 5. DeleteProductAsync - Xóa sản phẩm
//        [Fact]
//        public async Task DeleteProductAsync_ShouldCallRepositoryDeleteAsync_WhenProductExists()
//        {
//            // Arrange
//            int productId = 1;
//            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
//                .ReturnsAsync(new Product());

//            _productRepositoryMock.Setup(repo => repo.DeleteAsync(productId))
//                .Returns(Task.CompletedTask);

//            // Act
//            await _productService.DeleteProductAsync(productId);

//            // Assert
//            _productRepositoryMock.Verify(repo => repo.DeleteAsync(productId), Times.Once);
//        }

//        [Fact]
//        public async Task DeleteProductAsync_ShouldThrowException_WhenProductDoesNotExist()
//        {
//            // Arrange
//            int productId = 1;
//            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
//                .ReturnsAsync((Product)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<Exception>(() => _productService.DeleteProductAsync(productId));
//        }
//    }
//}
