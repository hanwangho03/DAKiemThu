using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Service;

namespace WebDoDienTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();

            foreach (var pro in products)
            {
                if (pro.ReleaseDate > DateTime.UtcNow)
                {
                    ViewData["ProductRelease"] = pro;
                }
            }

            ViewData["Categories"] = await _categoryService.GetCategoriesAsync();
            return View(products);
        }
    }
}
