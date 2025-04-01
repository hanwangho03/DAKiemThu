using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface IHomeService
    {
        Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task<int> GetTotalProductsAsync();
        Task<int> GetTotalOrdersAsync();
        Task<int> GetTotalUsersAsync();
        Task<IEnumerable<Order>> GetRecentOrdersAsync();
    }
}
