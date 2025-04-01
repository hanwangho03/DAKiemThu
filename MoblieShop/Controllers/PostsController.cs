using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using System.Security.Claims;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostsController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            var comments = await _commentService.GetCommentsByPostIdAsync(id);
            var postContentAsHtml = Markdown.ToHtml(post.Content);

            // Lấy userId của người dùng đang đăng nhập
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userAction = post.ActionPosts.FirstOrDefault(ap => ap.UserId == userId);

            var viewModel = new PostDetailsViewModel
            {
                Post = post,
                PostContentAsHtml = postContentAsHtml,
                Comments = comments,
                UserLiked = userAction?.Like ?? false,
                UserDisliked = userAction?.Dislike ?? false
            };

            return View(viewModel);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LikePost([FromBody] int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postService.HandleLikePostAsync(postId, userId);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DislikePost([FromBody] int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postService.HandleDislikePostAsync(postId, userId);

            return Ok();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveLike([FromBody] int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postService.HandleRemoveLikeAsync(postId, userId);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveDislike([FromBody] int postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _postService.HandleRemoveLikeAsync(postId, userId);

            return Ok();
        }

    }
}
