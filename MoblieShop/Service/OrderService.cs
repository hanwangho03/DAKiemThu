using MoblieShop.Repository;
using WebDoDienTu.Models;

namespace WebDoDienTu.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<Order> CreateOrderAsync(Order order, string paymentMethod)
        {
            order.Status = OrderStatus.Pending;
            await _orderRepository.AddOrderAsync(order);
            return order;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            await _orderRepository.UpdateOrderStatusAsync(orderId, status);
        }
    }
}
