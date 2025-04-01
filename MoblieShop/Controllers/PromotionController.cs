using Microsoft.AspNetCore.Mvc;
using MoblieShop.Service;

namespace WebDoDienTu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotions = await _promotionService.GetAllPromotionsAsync();
            return Ok(promotions);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetPromotion(string code)
        {
            var promotion = await _promotionService.ValidatePromotionCodeAsync(code);

            if (promotion == null)
            {
                return NotFound(new { message = "Mã giảm giá không hợp lệ hoặc đã hết hạn." });
            }

            return Ok(promotion);
        }
    }
}
