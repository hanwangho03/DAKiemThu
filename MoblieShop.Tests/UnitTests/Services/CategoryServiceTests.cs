//using MoblieShop.Tests.MockData;
//using Moq;
//using WebDoDienTu.Models;
//using WebDoDienTu.Repository;
//using WebDoDienTu.Service;

//namespace MoblieShop.Tests.UnitTests.Services
//{
//    public class CategoryServiceTests
//    {
//        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
//        private readonly CategoryService _categoryService;

//        public CategoryServiceTests()
//        {
//            _categoryRepositoryMock = new Mock<ICategoryRepository>();
//            _categoryService = new CategoryService(_categoryRepositoryMock.Object);
//        }

//        [Fact]
//        public async Task GetCategoriesAsync_ShouldReturnCategoryList()
//        {
//            // Arrange
//            var categories = CategoryMockData.GetCategories();
//            _categoryRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);

//            // Act
//            var result = await _categoryService.GetCategoriesAsync();

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(categories.Count, ((List<Category>)result).Count);
//        }

//        [Fact]
//        public async Task GetCategoryByIdAsync_ShouldReturnCorrectCategory()
//        {
//            // Arrange
//            var categories = CategoryMockData.GetCategories();
//            var category = categories[0];
//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(category.CategoryId)).ReturnsAsync(category);

//            // Act
//            var result = await _categoryService.GetCategoryByIdAsync(category.CategoryId);

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(category.CategoryId, result.CategoryId);
//            Assert.Equal(category.CategoryName, result.CategoryName);
//        }

//        [Fact]
//        public async Task GetCategoryByIdAsync_ShouldThrowException_WhenIdDoesNotExist()
//        {
//            // Arrange
//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<KeyNotFoundException>(() => _categoryService.GetCategoryByIdAsync(999));
//        }

//        [Fact]
//        public async Task GetCategoryByIdAsync_ShouldThrowException_WhenIdIsInvalid()
//        {
//            // Act & Assert
//            await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.GetCategoryByIdAsync(-1));
//        }

//        [Fact]
//        public async Task CreateCategoryAsync_ShouldCallRepository()
//        {
//            // Arrange
//            var newCategory = new Category { CategoryName = "Tablet" };
//            var existingCategory = new Category { CategoryId = 1, CategoryName = "Tablet" };

//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingCategory.CategoryId)).ReturnsAsync(existingCategory);

//            // Act
//            await _categoryService.CreateCategoryAsync(newCategory);

//            // Assert
//            _categoryRepositoryMock.Verify(r => r.AddAsync(newCategory), Times.Once);
//        }

//        [Fact]
//        public async Task CreateCategoryAsync_ShouldThrowException_WhenNameIsEmpty()
//        {
//            // Arrange
//            var newCategory = new Category { CategoryName = "" };

//            // Act & Assert
//            await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.CreateCategoryAsync(newCategory));
//        }

//        [Fact]
//        public async Task CreateCategoryAsync_ShouldThrowException_WhenCategoryExists()
//        {
//            // Arrange
//            var existingCategory = CategoryMockData.GetCategories()[0];
//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingCategory.CategoryId)).ReturnsAsync(existingCategory);

//            // Act & Assert
//            await Assert.ThrowsAsync<InvalidOperationException>(() => _categoryService.CreateCategoryAsync(existingCategory));
//        }

//        [Fact]
//        public async Task UpdateCategoryAsync_ShouldCallRepository()
//        {
//            // Arrange
//            var existingCategory = CategoryMockData.GetCategories()[0];
//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingCategory.CategoryId)).ReturnsAsync(existingCategory);

//            // Act
//            await _categoryService.UpdateCategoryAsync(existingCategory);

//            // Assert
//            _categoryRepositoryMock.Verify(r => r.UpdateAsync(existingCategory), Times.Once);
//        }

//        [Fact]
//        public async Task UpdateCategoryAsync_ShouldThrowException_WhenCategoryDoesNotExist()
//        {
//            // Arrange
//            var nonExistentCategory = new Category { CategoryId = 999, CategoryName = "Gaming" };
//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(nonExistentCategory.CategoryId)).ReturnsAsync((Category)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<KeyNotFoundException>(() => _categoryService.UpdateCategoryAsync(nonExistentCategory));
//        }

//        [Fact]
//        public async Task UpdateCategoryAsync_ShouldThrowException_WhenNameIsEmpty()
//        {
//            // Arrange
//            var invalidCategory = new Category { CategoryId = 1, CategoryName = "" };
//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(invalidCategory.CategoryId)).ReturnsAsync(invalidCategory);

//            // Act & Assert
//            await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.UpdateCategoryAsync(invalidCategory));
//        }

//        [Fact]
//        public async Task DeleteCategoryAsync_ShouldCallRepository()
//        {
//            // Arrange
//            var existingCategory = CategoryMockData.GetCategories()[0];
//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(existingCategory.CategoryId)).ReturnsAsync(existingCategory);

//            // Act
//            await _categoryService.DeleteCategoryAsync(existingCategory.CategoryId);

//            // Assert
//            _categoryRepositoryMock.Verify(r => r.DeleteAsync(existingCategory.CategoryId), Times.Once);
//        }

//        [Fact]
//        public async Task DeleteCategoryAsync_ShouldThrowException_WhenCategoryDoesNotExist()
//        {
//            // Arrange
//            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category)null);

//            // Act & Assert
//            await Assert.ThrowsAsync<KeyNotFoundException>(() => _categoryService.DeleteCategoryAsync(999));
//        }

//        [Fact]
//        public async Task DeleteCategoryAsync_ShouldThrowException_WhenIdIsInvalid()
//        {
//            // Act & Assert
//            await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.DeleteCategoryAsync(-1));
//        }
//    }
//}
