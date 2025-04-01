using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface IReviewService
    {
        Task<IEnumerable<ProductReview>> GetAllReviewsAsync();
        Task<bool> ToggleReviewVisibilityAsync(int reviewId);
    }
}
