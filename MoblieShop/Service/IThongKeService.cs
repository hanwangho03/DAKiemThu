namespace MoblieShop.Service
{
    public interface IThongKeService
    {
        Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync();
        Task<string> GetTopSellingProductsJsonAsync();
    }
}
