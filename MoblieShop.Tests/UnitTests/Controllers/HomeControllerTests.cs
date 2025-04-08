//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using WebDoDienTu.Controllers;
//using WebDoDienTu.Models;
//using WebDoDienTu.Service;

//namespace MoblieShop.Tests.UnitTests.Controllers
//{
//    public class HomeControllerTests
//    {
//        private readonly Mock<IProductService> _productServiceMock;
//        private readonly Mock<ICategoryService> _categoryServiceMock;
//        private readonly HomeController _controller;

//        public HomeControllerTests()
//        {
//            _productServiceMock = new Mock<IProductService>();
//            _categoryServiceMock = new Mock<ICategoryService>();
//            _controller = new HomeController(_productServiceMock.Object, _categoryServiceMock.Object);
//        }

//        // ✅ Trả về View với danh sách sản phẩm bình thường
//        [Fact]
//        public async Task Index_ShouldReturnViewWithProducts()
//        {
//            var mockProducts = new List<Product>
//            {
//                new Product { ProductId = 1, ProductName = "Laptop", ReleaseDate = DateTime.UtcNow.AddDays(-5) },
//                new Product { ProductId = 2, ProductName = "Smartphone", ReleaseDate = DateTime.UtcNow.AddDays(2) }
//            };
//            var mockCategories = new List<Category> { new Category { CategoryId = 1, CategoryName = "Electronics" } };

//            _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(mockProducts);
//            _categoryServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(mockCategories);

//            var result = await _controller.Index() as ViewResult;

//            Assert.NotNull(result);
//            Assert.IsType<List<Product>>(result.Model);
//            var model = Assert.IsAssignableFrom<List<Product>>(result.Model);
//            Assert.Equal(2, model.Count);

//            // Kiểm tra sản phẩm có ReleaseDate trong tương lai
//            Assert.True(result.ViewData.ContainsKey("ProductRelease"));
//            var productRelease = result.ViewData["ProductRelease"] as Product;
//            Assert.NotNull(productRelease);
//            Assert.Equal("Smartphone", productRelease.ProductName);
//        }

//        // 🆕 ✅ Trả về View với danh sách sản phẩm rỗng
//        [Fact]
//        public async Task Index_ShouldReturnViewWithEmptyProductList()
//        {
//            _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(new List<Product>());
//            _categoryServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(new List<Category>());

//            var result = await _controller.Index() as ViewResult;

//            Assert.NotNull(result);
//            Assert.Empty(result.Model as List<Product>);
//            Assert.False(result.ViewData.ContainsKey("ProductRelease"));
//            Assert.Empty(result.ViewData["Categories"] as List<Category>);
//        }

//        // 🆕 ✅ Không có sản phẩm nào có ngày phát hành trong tương lai
//        [Fact]
//        public async Task Index_ShouldNotSetProductRelease_WhenNoFutureProducts()
//        {
//            var mockProducts = new List<Product>
//            {
//                new Product { ProductId = 1, ProductName = "Laptop", ReleaseDate = DateTime.UtcNow.AddDays(-5) }
//            };

//            _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(mockProducts);
//            _categoryServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(new List<Category>());

//            var result = await _controller.Index() as ViewResult;

//            Assert.NotNull(result);
//            Assert.Single(result.Model as List<Product>);
//            Assert.False(result.ViewData.ContainsKey("ProductRelease"));
//        }

//        // 🆕 ✅ Không có danh mục nào trong hệ thống
//        [Fact]
//        public async Task Index_ShouldReturnEmptyCategories_WhenNoCategoriesExist()
//        {
//            var mockProducts = new List<Product> { new Product { ProductId = 1, ProductName = "Laptop", ReleaseDate = DateTime.UtcNow.AddDays(-5) } };

//            _productServiceMock.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(mockProducts);
//            _categoryServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(new List<Category>());

//            var result = await _controller.Index() as ViewResult;

//            Assert.NotNull(result);
//            Assert.Single(result.Model as List<Product>);
//            Assert.True(result.ViewData.ContainsKey("Categories"));
//            Assert.Empty(result.ViewData["Categories"] as List<Category>);
//        }

//        // 🆕 ✅ Xảy ra exception từ ProductService
//        [Fact]
//        public async Task Index_ShouldHandleException_FromProductService()
//        {
//            _productServiceMock.Setup(s => s.GetAllProductsAsync()).ThrowsAsync(new Exception("Lỗi khi tải sản phẩm"));
//            _categoryServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(new List<Category>());

//            var result = await _controller.Index() as ViewResult;

//            Assert.NotNull(result);
//            Assert.Null(result.Model);
//            Assert.False(result.ViewData.ContainsKey("ProductRelease"));
//        }
//    }
//}
