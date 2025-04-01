using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IProductReviewRepository
    {
        Task AddReviewAsync(ProductReview review);
    }
}
