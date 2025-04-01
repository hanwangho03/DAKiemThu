using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<ProductReview>> GetAllReviewsAsync();
        Task<ProductReview?> GetReviewByIdAsync(int id);
        Task UpdateReviewAsync(ProductReview review);
    }
}
