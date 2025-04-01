using MoblieShop.Repository;
using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;

        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync()
        {
            return await _homeRepository.GetMonthlyRevenueAsync();
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _homeRepository.GetTotalRevenueAsync();
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _homeRepository.GetTotalProductsAsync();
        }

        public async Task<int> GetTotalOrdersAsync()
        {
            return await _homeRepository.GetTotalOrdersAsync();
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _homeRepository.GetTotalUsersAsync();
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersAsync()
        {
            return await _homeRepository.GetRecentOrdersAsync();
        }
    }
}
