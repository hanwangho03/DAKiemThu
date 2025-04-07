using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoblieShop.Service;
using WebDoDienTu.Data;
using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _userService.CreateUserAsync(model);

            if (result.Succeeded)
                return RedirectToAction("Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetEditUserViewModelAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _userService.UpdateUserAsync(model))
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Cập nhật người dùng thất bại.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _userService.DeleteUserAsync(id))
                return RedirectToAction("Index");

            TempData["ErrorMessage"] = "Không thể xóa người dùng có đơn hàng.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> BlockUser(string userId)
        {
            if (await _userService.BlockUserAsync(userId))
                return RedirectToAction("Index");

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUser(string userId)
        {
            if (await _userService.UnblockUserAsync(userId))
                return RedirectToAction("Index");

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string role)
        {
            if (await _userService.ChangeUserRoleAsync(userId, role))
            {
                TempData["Message"] = "Cập nhật vai trò thành công!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Lỗi khi cập nhật vai trò.");
            return RedirectToAction("Index");
        }
    }
}
