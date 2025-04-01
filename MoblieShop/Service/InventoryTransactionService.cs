using MoblieShop.Repository;
using WebDoDienTu.Models;
using WebDoDienTu.Repository;

namespace MoblieShop.Service
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly IInventoryTransactionRepository _transactionRepository;
        private readonly IProductRepository _productRepository;

        public InventoryTransactionService(IInventoryTransactionRepository transactionRepository, IProductRepository productRepository)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<bool> CreateTransactionAsync(InventoryTransaction transaction)
        {
            var product = await _productRepository.GetByIdAsync(transaction.ProductId);
            if (product == null) return false;

            transaction.TransactionDate = DateTime.Now;
            await _transactionRepository.AddTransactionAsync(transaction);

            if (transaction.TransactionType == TransactionType.Import)
            {
                product.StockQuantity += transaction.Quantity;
            }
            else if (transaction.TransactionType == TransactionType.Export && product.StockQuantity >= transaction.Quantity)
            {
                product.StockQuantity -= transaction.Quantity;
            }
            else
            {
                return false;
            }

            await _productRepository.UpdateAsync(product);
            return true;
        }
    }
}
