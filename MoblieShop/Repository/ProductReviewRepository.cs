using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(ProductReview review)
        {
            _context.ProductReviews.Add(review);
            await _context.SaveChangesAsync();
        }
    }
}
