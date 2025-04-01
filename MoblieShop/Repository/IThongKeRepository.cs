using MoblieShop.ViewModels;

namespace MoblieShop.Repository
{
    public interface IThongKeRepository
    {
        Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync();
        Task<List<(string ProductName, int TotalQuantity)>> GetTopSellingProductsAsync();
    }
}
