using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface IOrderComplaintService
    {
        Task<OrderComplaint?> GetComplaintFormAsync(int orderId);
        Task<bool> SubmitComplaintAsync(OrderComplaint complaint, string userId);
        Task<IEnumerable<OrderComplaint>> GetAllComplaintsAsync();
        Task<bool> ResolveComplaintAsync(int id, OrderComplaintStatus status, string adminResponse);
    }
}
