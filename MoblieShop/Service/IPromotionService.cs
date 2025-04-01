using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface IPromotionService
    {
        Task<List<Promotion>> GetAllPromotionsAsync();
        Task<Promotion?> ValidatePromotionCodeAsync(string code);
        Task<Promotion?> GetPromotionByIdAsync(int id);
        Task AddPromotionAsync(Promotion promotion);
        Task UpdatePromotionAsync(Promotion promotion);
        Task DeletePromotionAsync(int id);
    }
}
