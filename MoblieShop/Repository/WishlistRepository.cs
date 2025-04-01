using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _context;

        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WishList?> GetWishlistByUserIdAsync(string userId)
        {
            return await _context.WishLists
                .Include(w => w.WishListItems)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task AddWishlistAsync(WishList wishlist)
        {
            _context.WishLists.Add(wishlist);
            await _context.SaveChangesAsync();
        }

        public async Task AddWishlistItemAsync(WishListItem wishlistItem)
        {
            _context.WishListItems.Add(wishlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveWishlistItemAsync(int productId, string userId)
        {
            var wishlist = await GetWishlistByUserIdAsync(userId);
            if (wishlist == null) return;

            var item = wishlist.WishListItems.FirstOrDefault(p => p.ProductId == productId);
            if (item != null)
            {
                _context.WishListItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
