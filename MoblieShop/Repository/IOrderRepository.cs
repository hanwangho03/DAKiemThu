using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task AddOrderAsync(Order order);
        Task<Order> CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<decimal> GetTotalRevenueAsync();
        Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync();
        Task<int> GetTotalOrdersAsync();
    }
}
