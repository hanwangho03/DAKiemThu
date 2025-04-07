using WebDoDienTu.Models;
using WebDoDienTu.Repository;

namespace WebDoDienTu.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("ID không hợp lệ.");
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Danh mục không tồn tại.");
            }

            return category;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Danh mục không được null.");
            }

            if (string.IsNullOrEmpty(category.CategoryName))
            {
                throw new ArgumentException("Tên danh mục không thể để trống.");
            }

            var existingCategory = await _categoryRepository.GetByNameAsync(category.CategoryName);
            if (existingCategory != null)
            {
                throw new InvalidOperationException("Tên danh mục đã tồn tại.");
            }

            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Danh mục không được null.");
            }

            if (category.CategoryId <= 0)
            {
                throw new ArgumentException("ID không hợp lệ.");
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(category.CategoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Danh mục không tồn tại.");
            }

            if (string.IsNullOrEmpty(category.CategoryName))
            {
                throw new ArgumentException("Tên danh mục không thể để trống.");
            }

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("ID không hợp lệ.");
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Danh mục không tồn tại.");
            }

            await _categoryRepository.DeleteAsync(categoryId);
        }
    }
}
