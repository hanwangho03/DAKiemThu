using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using WebDoDienTu.Models;

namespace WebDoDienTu.Controllers
{
    public class OrderComplaintsController : Controller
    {
        private readonly IOrderComplaintService _orderComplaintService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderComplaintsController(IOrderComplaintService orderComplaintService, UserManager<ApplicationUser> userManager)
        {
            _orderComplaintService = orderComplaintService;
            _userManager = userManager;
        }

        // Hiển thị form khiếu nại cho đơn hàng
        public async Task<IActionResult> Report(int orderId)
        {
            var complaint = await _orderComplaintService.GetComplaintFormAsync(orderId);
            if (complaint == null) return NotFound();

            return View(complaint);
        }

        // Xử lý khi người dùng gửi khiếu nại cho đơn hàng
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitComplaint(OrderComplaint complaint)
        {
            if (!ModelState.IsValid)
            {
                return View("Report", complaint);
            }

            var userId = _userManager.GetUserId(User);
            var success = await _orderComplaintService.SubmitComplaintAsync(complaint, userId);

            if (success)
            {
                TempData["Message"] = "Your complaint has been submitted successfully.";
                return RedirectToAction("Report", new { orderId = complaint.OrderId });
            }

            ModelState.AddModelError("", "Failed to submit the complaint.");
            return View("Report", complaint);
        }
    }
}
