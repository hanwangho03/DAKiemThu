namespace MoblieShop.Service
{
    public interface IProductReviewService
    {
        Task<bool> AddReviewAsync(int productId, string name, string email, int rating, string comment, string userId);
    }
}
