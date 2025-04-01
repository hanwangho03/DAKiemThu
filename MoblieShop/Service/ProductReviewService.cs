using MoblieShop.Repository;
using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _reviewRepository;

        public ProductReviewService(IProductReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> AddReviewAsync(int productId, string name, string email, int rating, string comment, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            var review = new ProductReview
            {
                ProductId = productId,
                UserId = userId,
                YourName = name,
                YourEmail = email,
                Rating = rating,
                Comment = comment
            };

            await _reviewRepository.AddReviewAsync(review);
            return true;
        }
    }
}
