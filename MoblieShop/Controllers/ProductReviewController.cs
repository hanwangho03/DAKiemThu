using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using System.Security.Claims;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace WebDoDienTu.Controllers
{
    public class ProductReviewController : Controller
    {
        private readonly IProductReviewService _reviewService;

        public ProductReviewController(IProductReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int productId, string name, string email, int rating, string comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để thực hiện đánh giá!" });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reviewService.AddReviewAsync(productId, name, email, rating, comment, userId);

            if (!result)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi gửi đánh giá!" });
            }

            return Json(new { success = true, message = "Đánh giá của bạn đã được gửi thành công!" });
        }
    }
}
