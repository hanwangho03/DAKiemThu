using MoblieShop.Repository;
using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ProductReview>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllReviewsAsync();
        }

        public async Task<bool> ToggleReviewVisibilityAsync(int reviewId)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
            if (review == null) return false;

            review.IsHidden = !review.IsHidden;
            await _reviewRepository.UpdateReviewAsync(review);
            return true;
        }
    }
}
