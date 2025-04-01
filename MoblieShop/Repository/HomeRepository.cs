using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
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

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Orders.SumAsync(order => order.TotalPrice);
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTotalOrdersAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<int> GetTotalUsersAsync()
        {
            var adminRoleId = await _context.Roles
                .Where(role => role.Name == SD.Role_Admin)
                .Select(role => role.Id)
                .FirstOrDefaultAsync();

            return await _context.Users
                .Where(user => !_context.UserRoles
                    .Where(role => role.RoleId == adminRoleId)
                    .Select(role => role.UserId)
                    .Contains(user.Id))
                .CountAsync();
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersAsync()
        {
            return await _context.Orders.OrderByDescending(o => o.OrderDate).Take(10).ToListAsync();
        }
    }
}
