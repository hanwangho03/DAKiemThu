using WebDoDienTu.Extensions;
using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public class CartService : ICartService
    {
        private const string CartSessionKey = "Cart";

        public ShoppingCart GetCart(HttpContext context)
        {
            return context.Session.GetObjectFromJson<ShoppingCart>(CartSessionKey) ?? new ShoppingCart();
        }

        public void AddToCart(HttpContext context, int productId, int quantity)
        {
            var cart = GetCart(context);
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
            }

            context.Session.SetObjectAsJson(CartSessionKey, cart);
        }

        public void RemoveFromCart(HttpContext context, int productId)
        {
            var cart = GetCart(context);
            cart.Items.RemoveAll(item => item.ProductId == productId);
            context.Session.SetObjectAsJson(CartSessionKey, cart);
        }

        public void UpdateCart(HttpContext context, int productId, int quantity)
        {
            var cart = GetCart(context);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (quantity > 0) item.Quantity = quantity;
                else cart.Items.Remove(item);
            }
            context.Session.SetObjectAsJson(CartSessionKey, cart);
        }

        public void ClearCart(HttpContext context)
        {
            context.Session.Remove(CartSessionKey);
        }
    }
}
