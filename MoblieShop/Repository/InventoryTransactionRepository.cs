using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetAllTransactionsAsync()
        {
            return await _context.InventoryTransactions.Include(t => t.Product).ToListAsync();
        }

        public async Task AddTransactionAsync(InventoryTransaction transaction)
        {
            await _context.InventoryTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
