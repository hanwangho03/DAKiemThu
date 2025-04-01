using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;
using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PromotionController : Controller
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionService.GetAllPromotionsAsync();
            PromotionViewModel promotionViewModel = new PromotionViewModel { Promotions = promotions };
            return View(promotionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPromotion(Promotion promotion)
        {
            await _promotionService.AddPromotionAsync(promotion);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(id);
            return View(promotion);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Promotion promotion)
        {
            if (promotion.Id != id)
            {
                return NotFound();
            }
            await _promotionService.UpdatePromotionAsync(promotion);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _promotionService.DeletePromotionAsync(id);
            return RedirectToAction("Index");
        }
    }
}
