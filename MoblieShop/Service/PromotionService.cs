using MoblieShop.Repository;
using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<List<Promotion>> GetAllPromotionsAsync()
        {
            return await _promotionRepository.GetAllPromotionsAsync();
        }

        public async Task<Promotion?> ValidatePromotionCodeAsync(string code)
        {
            return await _promotionRepository.GetPromotionByCodeAsync(code);
        }

        public async Task<Promotion?> GetPromotionByIdAsync(int id)
        {
            return await _promotionRepository.GetPromotionByIdAsync(id);
        }

        public async Task AddPromotionAsync(Promotion promotion)
        {
            await _promotionRepository.AddPromotionAsync(promotion);
        }

        public async Task UpdatePromotionAsync(Promotion promotion)
        {
            await _promotionRepository.UpdatePromotionAsync(promotion);
        }

        public async Task DeletePromotionAsync(int id)
        {
            await _promotionRepository.DeletePromotionAsync(id);
        }
    }
}
