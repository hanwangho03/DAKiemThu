using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;

namespace MoblieShop.Repository
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly ApplicationDbContext _context;

        public ThongKeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync()
        {
            var revenueData = await _context.Orders
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(o => o.TotalPrice)
                })
                .ToListAsync();

            var revenueByMonth = new Dictionary<int, decimal>();
            for (int i = 1; i <= 12; i++)
            {
                revenueByMonth[i] = revenueData
                    .Where(x => x.Month == i)
                    .Sum(x => x.Revenue);
            }

            return revenueByMonth;
        }

        public async Task<List<(string ProductName, int TotalQuantity)>> GetTopSellingProductsAsync()
        {
            var topProducts = await _context.OrderDetails
                .GroupBy(od => od.Product)
                .Select(g => new
                {
                    ProductName = g.Key.ProductName,
                    TotalQuantity = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .ToListAsync();

            return topProducts.Select(x => (x.ProductName, x.TotalQuantity)).ToList();
        }
    }
}
