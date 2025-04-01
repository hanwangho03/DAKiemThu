using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderComplaintsController : Controller
    {
        private readonly IOrderComplaintService _complaintService;

        public OrderComplaintsController(IOrderComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        // Danh sách khiếu nại 
        public async Task<IActionResult> ComplaintList()
        {
            var complaints = await _complaintService.GetAllComplaintsAsync();
            return View(complaints);
        }

        // Xử lý khiếu nại
        [HttpPost]
        public async Task<IActionResult> ResolveComplaint(int id, OrderComplaintStatus orderComplaintStatus, string adminResponse)
        {
            bool success = await _complaintService.ResolveComplaintAsync(id, orderComplaintStatus, adminResponse);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("ComplaintList");
        }
    }
}
