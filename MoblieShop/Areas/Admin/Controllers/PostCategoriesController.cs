using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PostCategoriesController : Controller
    {
        private readonly IPostCategoryService _categoryService;

        public PostCategoriesController(IPostCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Admin/PostCategories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        // POST: Admin/PostCategories/Create
        [HttpPost]
        public async Task<JsonResult> Create([FromBody] PostCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(category);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Model không hợp lệ." });
        }

        // POST: Admin/PostCategories/Edit
        [HttpPost]
        public async Task<JsonResult> Edit([FromBody] PostCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Model không hợp lệ." });
        }

        // POST: Admin/PostCategories/Delete
        [HttpPost]
        public async Task<JsonResult> Delete([FromBody] int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "ID không hợp lệ." });
            }

            var categoryExists = await _categoryService.GetCategoryByIdAsync(id);
            if (categoryExists == null)
            {
                return Json(new { success = false, message = "Danh mục không tồn tại." });
            }

            await _categoryService.DeleteCategoryAsync(id);
            return Json(new { success = true });
        }
    }
}
