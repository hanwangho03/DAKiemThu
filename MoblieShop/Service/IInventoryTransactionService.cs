using WebDoDienTu.Models;

namespace MoblieShop.Service
{
    public interface IInventoryTransactionService
    {
        Task<IEnumerable<InventoryTransaction>> GetAllTransactionsAsync();
        Task<bool> CreateTransactionAsync(InventoryTransaction transaction);
    }
}
