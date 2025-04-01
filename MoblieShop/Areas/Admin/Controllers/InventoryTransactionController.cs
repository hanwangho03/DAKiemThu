using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoblieShop.Service;
using WebDoDienTu.Models;
using WebDoDienTu.Service;

namespace WebDoDienTu.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class InventoryTransactionController : Controller
    {
        private readonly IInventoryTransactionService _transactionService;
        private readonly IProductService _productService;

        public InventoryTransactionController(IInventoryTransactionService transactionService, IProductService productService)
        {
            _transactionService = transactionService;
            _productService = productService;
        }

        // Hiển thị danh sách giao dịch kho
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return View(transactions);
        }

        // Hiển thị form thêm mới giao dịch kho
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["ProductId"] = new SelectList(await _productService.GetAllProductsAsync(), "ProductId", "ProductName");
            return View();
        }

        // Xử lý thêm mới giao dịch kho
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Quantity,TransactionType")] InventoryTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                bool success = await _transactionService.CreateTransactionAsync(transaction);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Invalid transaction. Check stock quantity.");
            }

            ViewData["ProductId"] = new SelectList(await _productService.GetAllProductsAsync(), "ProductId", "ProductName", transaction.ProductId);
            return View(transaction);
        }
    }
}
