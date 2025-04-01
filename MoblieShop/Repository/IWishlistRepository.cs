using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IWishlistRepository
    {
        Task<WishList?> GetWishlistByUserIdAsync(string userId);
        Task AddWishlistAsync(WishList wishlist);
        Task AddWishlistItemAsync(WishListItem wishlistItem);
        Task RemoveWishlistItemAsync(int productId, string userId);
    }
}
