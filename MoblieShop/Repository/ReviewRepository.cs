using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductReview>> GetAllReviewsAsync()
        {
            return await _context.ProductReviews.Include(r => r.Product).ToListAsync();
        }

        public async Task<ProductReview?> GetReviewByIdAsync(int id)
        {
            return await _context.ProductReviews.FindAsync(id);
        }

        public async Task UpdateReviewAsync(ProductReview review)
        {
            _context.ProductReviews.Update(review);
            await _context.SaveChangesAsync();
        }
    }
}
