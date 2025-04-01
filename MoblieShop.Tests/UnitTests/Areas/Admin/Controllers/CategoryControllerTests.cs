using Microsoft.AspNetCore.Mvc;
using Moq;
using WebDoDienTu.Areas.Admin.Controllers;
using WebDoDienTu.Models;
using WebDoDienTu.Service;

namespace MoblieShop.Tests.UnitTests.Areas.Admin.Controllers
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _controller = new CategoryController(_categoryServiceMock.Object);
        }

        // ✅ Test Index() trả về danh sách danh mục
        [Fact]
        public async Task Index_ShouldReturnViewWithCategories()
        {
            var categories = new List<Category> { new Category { CategoryId = 1, CategoryName = "Laptop" } };
            _categoryServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(categories);

            var result = await _controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result.Model);
            Assert.Single((List<Category>)result.Model);
        }

        // ✅ Test Index() khi danh sách rỗng
        [Fact]
        public async Task Index_ShouldReturnViewWithEmptyList()
        {
            _categoryServiceMock.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(new List<Category>());
            var result = await _controller.Index() as ViewResult;
            Assert.NotNull(result);
            Assert.Empty((List<Category>)result.Model);
        }

        // ✅ Test Create (GET)
        [Fact]
        public void Create_Get_ShouldReturnView()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        // ✅ Test Create (POST) với dữ liệu hợp lệ
        [Fact]
        public async Task Create_Post_ShouldRedirectToIndex_WhenModelIsValid()
        {
            var category = new Category { CategoryId = 1, CategoryName = "Smartphone" };
            _categoryServiceMock.Setup(s => s.CreateCategoryAsync(category)).Returns(Task.CompletedTask);

            var result = await _controller.Create(category) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        // ✅ Test Create (POST) với dữ liệu không hợp lệ
        [Fact]
        public async Task Create_Post_ShouldReturnView_WhenModelIsInvalid()
        {
            _controller.ModelState.AddModelError("CategoryName", "Required");
            var category = new Category();

            var result = await _controller.Create(category) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(category, result.Model);
        }

        // ✅ Test Details() với ID hợp lệ
        [Fact]
        public async Task Details_ShouldReturnView_WhenCategoryExists()
        {
            var category = new Category { CategoryId = 1, CategoryName = "Tablet" };
            _categoryServiceMock.Setup(s => s.GetCategoryByIdAsync(1)).ReturnsAsync(category);

            var result = await _controller.Details(1) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(category, result.Model);
        }

        // ✅ Test Details() với ID không hợp lệ
        [Fact]
        public async Task Details_ShouldReturnNotFound_WhenCategoryDoesNotExist()
        {
            _categoryServiceMock.Setup(s => s.GetCategoryByIdAsync(1)).ReturnsAsync((Category)null);
            var result = await _controller.Details(1);
            Assert.IsType<NotFoundResult>(result);
        }

        // ✅ Test Edit (GET) với danh mục hợp lệ
        [Fact]
        public async Task Edit_Get_ShouldReturnView_WhenCategoryExists()
        {
            var category = new Category { CategoryId = 1, CategoryName = "Gaming" };
            _categoryServiceMock.Setup(s => s.GetCategoryByIdAsync(1)).ReturnsAsync(category);

            var result = await _controller.Edit(1) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(category, result.Model);
        }

        // ✅ Test Edit (POST) với ID sai lệch
        [Fact]
        public async Task Edit_Post_ShouldReturnNotFound_WhenIdMismatch()
        {
            var category = new Category { CategoryId = 2, CategoryName = "Monitor" };
            var result = await _controller.Edit(1, category);
            Assert.IsType<NotFoundResult>(result);
        }

        // ✅ Test Delete (GET) khi danh mục không tồn tại
        [Fact]
        public async Task Delete_Get_ShouldReturnNotFound_WhenCategoryDoesNotExist()
        {
            _categoryServiceMock.Setup(s => s.GetCategoryByIdAsync(1)).ReturnsAsync((Category)null);
            var result = await _controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }

        // ✅ Test DeleteConfirm() với ID hợp lệ
        [Fact]
        public async Task DeleteConfirm_ShouldRedirectToIndex_WhenCategoryIsDeleted()
        {
            _categoryServiceMock.Setup(s => s.DeleteCategoryAsync(1)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteConfirm(1) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
