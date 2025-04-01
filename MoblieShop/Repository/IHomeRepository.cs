using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IHomeRepository
    {
        Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task<int> GetTotalProductsAsync();
        Task<int> GetTotalOrdersAsync();
        Task<int> GetTotalUsersAsync();
        Task<IEnumerable<Order>> GetRecentOrdersAsync();
    }
}
