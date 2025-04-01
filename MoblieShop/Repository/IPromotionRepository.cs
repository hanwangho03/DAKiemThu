using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>> GetAllPromotionsAsync();
        Task<Promotion?> GetPromotionByCodeAsync(string code);
        Task<Promotion?> GetPromotionByIdAsync(int id);
        Task AddPromotionAsync(Promotion promotion);
        Task UpdatePromotionAsync(Promotion promotion);
        Task DeletePromotionAsync(int id);
    }
}
