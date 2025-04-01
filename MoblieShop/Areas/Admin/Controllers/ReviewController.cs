using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> ManageReviewsAsync()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return View(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleReviewVisibilityAsync(int reviewId)
        {
            bool success = await _reviewService.ToggleReviewVisibilityAsync(reviewId);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction("ManageReviews");
        }
    }
}
