using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDoDienTu.Models;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ChatController : Controller
    {
        public ChatController()
        {
        }

        [HttpGet]
        public IActionResult AdminChat()
        {
            return View();
        }
    }
}
