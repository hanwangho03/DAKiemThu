using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var revenueByMonth = await _homeService.GetMonthlyRevenueAsync();
            var totalRevenue = await _homeService.GetTotalRevenueAsync();
            var totalProducts = await _homeService.GetTotalProductsAsync();
            var totalOrders = await _homeService.GetTotalOrdersAsync();
            var totalUsers = await _homeService.GetTotalUsersAsync();
            var recentOrders = await _homeService.GetRecentOrdersAsync();

            ViewBag.RevenueByMonth = revenueByMonth;
            ViewBag.TongDoanhSo = totalRevenue;
            ViewBag.TongSoLuongSP = totalProducts;
            ViewBag.TongDonHang = totalOrders;
            ViewBag.SoLuongUser = totalUsers;

            return View(recentOrders);
        }
    }
}
