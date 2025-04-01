using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using WebDoDienTu.Data;
using WebDoDienTu.Extensions;
using WebDoDienTu.Models;
using WebDoDienTu.Service;
using WebDoDienTu.Service.ProductRecommendation;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IProductViewService _productViewService;
        private readonly RecommendationService _recommendationService;
        private readonly ProductRecommendationService _productRecommendationService;

        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IProductService productService, IProductViewService productViewService,
            RecommendationService recommendationService, ProductRecommendationService productRecommendationService)
        {
            _context = context;
            _userManager = userManager;
            _productService = productService;
            _productViewService = productViewService;
            _recommendationService = recommendationService;
            _productRecommendationService = productRecommendationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? category, string? keywords, string? priceRange, int? page)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;

            var products = await _productService.GetProductsAsync(category, keywords, priceRange, pageNumber, pageSize);

            if (!products.Any())
            {
                TempData["NoProductsMessage"] = "No suitable products found.";
            }

            ViewBag.Category = category;
            ViewBag.Keywords = keywords;
            ViewBag.PriceRange = priceRange;

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _productViewService.RecordProductViewAsync(user.Id, id);
            }

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Lấy danh sách sản phẩm gợi ý
            var recommendedProducts = _productRecommendationService.GetRecommendedProducts(id);
            ViewBag.RecommendedProducts = recommendedProducts;

            var recommendations = await _recommendationService.GetProductRecommendations(user.Id);
            ViewBag.Recommendations = recommendations;
            ViewBag.AverageRating = (product?.Reviews != null && product.Reviews.Any()) ? product.Reviews.Average(x => x.Rating) : 0;

            return View(product);
        }

        public async Task<IActionResult> AddToComparison(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null) return NotFound();

            // Lấy danh sách so sánh hiện tại từ session
            List<Product> comparisonList = HttpContext.Session.GetObjectFromJson<List<Product>>("ComparisonList") ?? new List<Product>();

            // Kiểm tra nếu danh sách đã có sản phẩm và loại sản phẩm mới không khớp
            if (comparisonList.Any() && comparisonList[0].CategoryId != product.CategoryId)
            {
                TempData["ErrorMessage"] = "Chỉ có thể so sánh các sản phẩm cùng loại.";
                return RedirectToAction("Compare");
            }

            // Thêm sản phẩm vào danh sách so sánh nếu chưa có
            if (!comparisonList.Any(p => p.ProductId == productId))
            {
                comparisonList.Add(product);
                HttpContext.Session.SetObjectAsJson("ComparisonList", comparisonList);
            }

            return RedirectToAction("Compare");
        }

        public async Task<IActionResult> Compare()
        {
            var comparisonList = HttpContext.Session.GetObjectFromJson<List<Product>>("ComparisonList") ?? new List<Product>();

            //foreach (var product in comparisonList)
            //{
            //    _context.Entry(product).Collection(p => p.Attributes).Load();
            //}

            var viewModel = new ProductComparisonViewModel
            {
                ProductsToCompare = comparisonList
            };

            // Hiển thị thông báo lỗi (nếu có)
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }

            var user = await _userManager.GetUserAsync(User);
            var recommendations = await _recommendationService.GetProductRecommendations(user.Id);
            ViewBag.Recommendations = recommendations;

            return View(viewModel);
        }

        public IActionResult RemoveFromComparison(int productId)
        {
            var comparisonList = HttpContext.Session.GetObjectFromJson<List<Product>>("ComparisonList") ?? new List<Product>();
            var productToRemove = comparisonList.FirstOrDefault(p => p.ProductId == productId);

            if (productToRemove != null)
            {
                comparisonList.Remove(productToRemove);
                HttpContext.Session.SetObjectAsJson("ComparisonList", comparisonList);
            }

            return RedirectToAction("Compare");
        }

        public async Task<IActionResult> GetRecommendations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recommendations = await _recommendationService.GetProductRecommendations(userId);

            // Trả về view với danh sách gợi ý sản phẩm
            return View(recommendations);
        }
    }
}
