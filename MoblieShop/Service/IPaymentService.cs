using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface IPaymentService
    {
        Task<string> ProcessPaymentAsync(Order order, string paymentMethod);
    }
}
