using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface IPostCategoryService
    {
        Task<IEnumerable<PostCategory>> GetAllCategoriesAsync();
        Task<PostCategory?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(PostCategory category);
        Task UpdateCategoryAsync(PostCategory category);
        Task DeleteCategoryAsync(int id);
    }
}
