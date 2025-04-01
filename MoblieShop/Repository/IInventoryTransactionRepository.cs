using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IInventoryTransactionRepository
    {
        Task<IEnumerable<InventoryTransaction>> GetAllTransactionsAsync();
        Task AddTransactionAsync(InventoryTransaction transaction);
    }
}
