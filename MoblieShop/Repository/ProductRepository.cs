using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;
using X.PagedList.Extensions;
using X.PagedList;

namespace WebDoDienTu.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(x => x.Category).Include(p => p.Images).Include(p => p.Attributes).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.Category).Include(x => x.Images).Include(p => p.Attributes).Include(p => p.Reviews).FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetByNameAsync(string stringName)
        {
            return await _context.Products
                .FirstOrDefaultAsync(c => c.ProductName.ToLower() == stringName.ToLower());

        }

        public async Task RemoveImagesAsync(List<ProductImage> images)
        {
            _context.ProductImages.RemoveRange(images);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAttributesAsync(List<ProductAttribute> attributes)
        {
            _context.ProductAttributes.RemoveRange(attributes);
            await _context.SaveChangesAsync();
        }

        public async Task<IPagedList<Product>> GetProductsByCategoryAsync(string category, int pageNumber, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _context.Products
                    .Include(p => p.Category)
                    .AsEnumerable()
                    .Where(x => x.Category.CategoryName.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .ToPagedList(pageNumber, pageSize);
            });
        }

        public async Task<IPagedList<Product>> GetProductsByKeywordsAsync(string keywords, int pageNumber, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _context.Products
                    .Where(x => x.ProductName.Contains(keywords))
                    .ToPagedList(pageNumber, pageSize);
            });
        }

        public async Task<IPagedList<Product>> GetProductsByPriceRangeAsync(int minPrice, int maxPrice, int pageNumber, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _context.Products
                    .Where(x => x.Price >= minPrice && x.Price < maxPrice)
                    .ToPagedList(pageNumber, pageSize);
            });
        }

        public async Task<IPagedList<Product>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            return await Task.Run(() =>
            {
                return _context.Products.ToPagedList(pageNumber, pageSize);
            });
        }
    }
}
