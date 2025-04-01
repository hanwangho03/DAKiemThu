using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using System.Security.Claims;

namespace WebDoDienTu.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Comment(int postId, string content)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _commentService.AddCommentAsync(postId, content, currentUserId);

            return RedirectToAction("Details", "Posts", new { id = postId });
        }

        [HttpGet]
        public async Task<IActionResult> LoadMoreComments(int postId, int offset, int count = 5)
        {
            var result = await _commentService.GetMoreCommentsAsync(postId, offset, count);

            if (!result.Any())
            {
                return Json(new { message = "No more comments available" });
            }

            return Json(result);
        }
    }
}
