using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MoblieShop.Service;
using WebDoDienTu.Extensions;
using WebDoDienTu.Models;
using WebDoDienTu.Service;

namespace WebDoDienTu.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<WishlistController> _localizer;

        public WishlistController(IWishlistService wishlistService, IProductService productService, UserManager<ApplicationUser> userManager, IStringLocalizer<WishlistController> localizer)
        {
            _wishlistService = wishlistService;
            _productService = productService;
            _userManager = userManager;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var viewModel = await _wishlistService.GetWishlistAsync(user.Id);

            if (viewModel.WishListItems.Count() == 0)
            {
                TempData["Message"] = _localizer["WishlistEmptyMessage"];
                return View("Index");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            await _wishlistService.AddToWishlistAsync(user.Id, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishList(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            await _wishlistService.RemoveFromWishlistAsync(user.Id, productId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            var cartItem = new CartItem
            {
                ProductId = productId,
                NameProduct = product.ProductName,
                Image = product.ImageUrl ?? string.Empty,
                Price = product.Price,
                Quantity = quantity
            };
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.AddItem(cartItem);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index", "Cart");
        }
    }
}
