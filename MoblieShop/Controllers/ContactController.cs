using Microsoft.AspNetCore.Mvc;

namespace WebDoDienTu.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
