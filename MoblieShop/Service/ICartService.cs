using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface ICartService
    {
        ShoppingCart GetCart(HttpContext context);
        void AddToCart(HttpContext context, int productId, int quantity);
        void RemoveFromCart(HttpContext context, int productId);
        void UpdateCart(HttpContext context, int productId, int quantity);
        void ClearCart(HttpContext context);
    }
}
