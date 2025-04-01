using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly ApplicationDbContext _context;

        public PromotionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Promotion>> GetAllPromotionsAsync()
        {
            return await _context.Promotions.ToListAsync();
        }

        public async Task<Promotion?> GetPromotionByCodeAsync(string code)
        {
            return await _context.Promotions
                .FirstOrDefaultAsync(p => p.Code == code && p.IsActive
                                      && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
        }

        public async Task<Promotion?> GetPromotionByIdAsync(int id)
        {
            return await _context.Promotions.FindAsync(id);
        }

        public async Task AddPromotionAsync(Promotion promotion)
        {
            await _context.Promotions.AddAsync(promotion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePromotionAsync(Promotion promotion)
        {
            _context.Promotions.Update(promotion);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePromotionAsync(int id)
        {
            var promotion = await _context.Promotions.FindAsync(id);
            if (promotion != null)
            {
                _context.Promotions.Remove(promotion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
