using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IOrderComplaintRepository
    {
        Task<IEnumerable<OrderComplaint>> GetAllComplaintsAsync();
        Task<OrderComplaint?> GetComplaintByIdAsync(int id);
        Task AddComplaintAsync(OrderComplaint complaint);
        Task UpdateComplaintAsync(OrderComplaint complaint);
    }
}
