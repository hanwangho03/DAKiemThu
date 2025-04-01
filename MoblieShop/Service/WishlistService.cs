using MoblieShop.Repository;
using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace MoblieShop.Service
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<WishListViewModel> GetWishlistAsync(string userId)
        {
            var wishlist = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
            return new WishListViewModel
            {
                WishListItems = wishlist?.WishListItems ?? new List<WishListItem>()
            };
        }

        public async Task AddToWishlistAsync(string userId, int productId)
        {
            var wishlist = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
            if (wishlist == null)
            {
                wishlist = new WishList { UserId = userId };
                await _wishlistRepository.AddWishlistAsync(wishlist);
            }

            if (!wishlist.WishListItems.Any(wi => wi.ProductId == productId))
            {
                var wishlistItem = new WishListItem
                {
                    ProductId = productId,
                    WishListId = wishlist.Id,
                    AddedDate = DateTime.UtcNow
                };
                await _wishlistRepository.AddWishlistItemAsync(wishlistItem);
            }
        }

        public async Task RemoveFromWishlistAsync(string userId, int productId)
        {
            await _wishlistRepository.RemoveWishlistItemAsync(productId, userId);
        }
    }
}
