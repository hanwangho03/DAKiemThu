using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using WebDoDienTu.Models;


namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ThongKeController : Controller
    {
        private readonly IThongKeService _thongKeService;

        public ThongKeController(IThongKeService thongKeService)
        {
            _thongKeService = thongKeService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.RevenueByMonth = await _thongKeService.GetMonthlyRevenueAsync();
            var topSellingProductsJson = await _thongKeService.GetTopSellingProductsJsonAsync();

            ViewBag.ProductData = topSellingProductsJson;
            return View();
        }

        public async Task<IActionResult> MonthlyRevenue()
        {
            ViewBag.RevenueByMonth = await _thongKeService.GetMonthlyRevenueAsync();
            return View();
        }

        public async Task<IActionResult> MostPurchasedProducts()
        {
            ViewBag.ProductData = await _thongKeService.GetTopSellingProductsJsonAsync();
            return View();
        }
    }
}
