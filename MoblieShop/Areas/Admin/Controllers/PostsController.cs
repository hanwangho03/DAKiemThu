using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoblieShop.Service;
using MoblieShop.Service.Cloudinary;
using System.Security.Claims;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly IImageService _imageService;
        private readonly IMarkdownService _markdownService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(
            IPostService postService,
            IPostCategoryService postCategoryService,
            IImageService imageService,
            IMarkdownService markdownService,
            UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _postCategoryService = postCategoryService;
            _imageService = imageService;
            _markdownService = markdownService;
            _userManager = userManager;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _postCategoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        public async Task<IActionResult> Create(Post post, IFormFile imageFile)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            post.AuthorId = currentUserId;
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                post.Content = _markdownService.ConvertMarkdownToHtml(post.Content);
                if (imageFile != null)
                {
                    post.ImageUrl = await _imageService.SaveImageToCloudinaryAsync(imageFile);
                }

                await _postService.AddPostAsync(post);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _postCategoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(post);
        }

        // GET: Posts/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var categories = await _postCategoryService.GetAllCategoriesAsync();
            var user = await _userManager.FindByIdAsync(post.AuthorId);
            ViewBag.Author = user.Email;
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(post);
        }

        // POST: Posts/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Post post, IFormFile? imageFile)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            var existingPost = await _postService.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingPost.Title = post.Title;
                existingPost.Content = _markdownService.ConvertMarkdownToHtml(post.Content);
                existingPost.Content = post.Content;
                existingPost.CategoryId = post.CategoryId;
                existingPost.IsPublished = post.IsPublished;
                existingPost.UpdatedAt = DateTime.UtcNow;

                if (imageFile != null)
                {
                    existingPost.ImageUrl = await _imageService.SaveImageToCloudinaryAsync(imageFile);
                }

                await _postService.UpdatePostAsync(existingPost);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _postCategoryService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View(post);
        }


        // GET: Posts/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postService.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
