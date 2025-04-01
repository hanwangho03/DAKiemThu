using WebDoDienTu.Models;
using X.PagedList;

namespace WebDoDienTu.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task RemoveImagesAsync(List<ProductImage> images);
        Task RemoveAttributesAsync(List<ProductAttribute> attributes);
        Task<IPagedList<Product>> GetProductsByCategoryAsync(string category, int pageNumber, int pageSize);
        Task<IPagedList<Product>> GetProductsByKeywordsAsync(string keywords, int pageNumber, int pageSize);
        Task<IPagedList<Product>> GetProductsByPriceRangeAsync(int minPrice, int maxPrice, int pageNumber, int pageSize);
        Task<IPagedList<Product>> GetAllProductsAsync(int pageNumber, int pageSize);
    }
}
