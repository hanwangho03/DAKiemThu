using MoblieShop.Repository;
using System.Text.Json;

namespace MoblieShop.Service
{
    public class ThongKeService : IThongKeService
    {
        private readonly IThongKeRepository _thongKeRepository;

        public ThongKeService(IThongKeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }

        public async Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync()
        {
            return await _thongKeRepository.GetMonthlyRevenueAsync();
        }

        public async Task<string> GetTopSellingProductsJsonAsync()
        {
            var topProducts = await _thongKeRepository.GetTopSellingProductsAsync();
            var productNames = JsonSerializer.Serialize(topProducts.Select(x => x.ProductName).ToList());
            var productQuantities = JsonSerializer.Serialize(topProducts.Select(x => x.TotalQuantity).ToList());

            return JsonSerializer.Serialize(new { ProductNames = productNames, ProductQuantities = productQuantities });
        }
    }
}
