using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebDoDienTu.Models;
using WebDoDienTu.Service.ProductRecommendation;
using WebDoDienTu.Service;
using X.PagedList.Extensions;
using X.PagedList;
using WebDoDienTu.Controllers;

namespace MoblieShop.Tests.UnitTests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IProductViewService> _productViewServiceMock;
        private readonly Mock<RecommendationService> _recommendationServiceMock;
        private readonly Mock<ProductRecommendationService> _productRecommendationServiceMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productViewServiceMock = new Mock<IProductViewService>();
            _recommendationServiceMock = new Mock<RecommendationService>();
            _productRecommendationServiceMock = new Mock<ProductRecommendationService>();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            _controller = new ProductController(
                null, _userManagerMock.Object, _productServiceMock.Object, _productViewServiceMock.Object,
                _recommendationServiceMock.Object, _productRecommendationServiceMock.Object);
        }

        // ✅ Test Index trả về View với danh sách sản phẩm
        [Fact]
        public async Task Index_ShouldReturnViewWithProducts()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Laptop" },
                new Product { ProductId = 2, ProductName = "Smartphone" }
            }.ToPagedList(1, 9);

            _productServiceMock
                .Setup(service => service.GetProductsAsync(null, null, null, 1, 9))
                .ReturnsAsync(mockProducts);

            // Act
            var result = await _controller.Index(null, null, null, 1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<IPagedList<Product>>(result.Model);
            var model = result.Model as IPagedList<Product>;
            Assert.Equal(2, model.Count);
        }

        // ✅ Test Index khi không có sản phẩm
        [Fact]
        public async Task Index_ShouldSetTempData_WhenNoProducts()
        {
            // Arrange
            var emptyList = new List<Product>().ToPagedList(1, 9);

            _productServiceMock
                .Setup(service => service.GetProductsAsync(null, null, null, 1, 9))
                .ReturnsAsync(emptyList);

            // Act
            var result = await _controller.Index(null, null, null, 1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(_controller.TempData.ContainsKey("NoProductsMessage"));
            Assert.Equal("No suitable products found.", _controller.TempData["NoProductsMessage"]);
        }
    }
}
