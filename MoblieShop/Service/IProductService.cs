using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;
using X.PagedList;

namespace WebDoDienTu.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateProductAsync(ProductCreateViewModel viewModel);
        Task UpdateProductAsync(ProductUpdateViewModel viewModel);
        Task DeleteProductAsync(int id);
        Task<IPagedList<Product>> GetProductsAsync(string category, string keywords, string priceRange, int pageNumber, int pageSize);
    }
}
