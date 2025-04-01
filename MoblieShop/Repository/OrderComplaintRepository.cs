using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public class OrderComplaintRepository : IOrderComplaintRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderComplaintRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderComplaint>> GetAllComplaintsAsync()
        {
            return await _context.OrderComplaints.Include(c => c.Order).Include(c => c.User).ToListAsync();
        }

        public async Task<OrderComplaint?> GetComplaintByIdAsync(int id)
        {
            return await _context.OrderComplaints.FindAsync(id);
        }

        public async Task AddComplaintAsync(OrderComplaint complaint)
        {
            await _context.OrderComplaints.AddAsync(complaint);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComplaintAsync(OrderComplaint complaint)
        {
            _context.OrderComplaints.Update(complaint);
            await _context.SaveChangesAsync();
        }
    }
}
