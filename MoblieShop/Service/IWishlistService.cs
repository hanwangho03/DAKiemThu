using WebDoDienTu.ViewModels;

namespace MoblieShop.Service
{
    public interface IWishlistService
    {
        Task<WishListViewModel> GetWishlistAsync(string userId);
        Task AddToWishlistAsync(string userId, int productId);
        Task RemoveFromWishlistAsync(string userId, int productId);
    }
}
