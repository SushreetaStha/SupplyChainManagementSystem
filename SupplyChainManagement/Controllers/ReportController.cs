using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyChainManagement.Data;
using SupplyChainManagement.ViewModels.Report;

namespace SupplyChainManagement.Controllers
{
    public class ReportController : Controller
    {
        private readonly DatabaseContext _db;

        public ReportController(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> SalesReport()
        {
            var model = new SalesReportViewModel();

            var orders = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Transactions)
                    .ThenInclude(t => t.Product)
                .Where(o => o.IsCompleted)
                .ToListAsync();

            foreach (var order in orders)
            {
                var salesOrder = new SalesOrderDetail
                {
                    CustomerName = order.Customer.CustomerName,
                    OrderItemsCount = order.Transactions.Count(),
                    Date = order.Date
                };

                foreach (var transaction in order.Transactions)
                {
                    var salesTransaction = new SalesTransactionDetail
                    {
                        ProductName = transaction.Product.ProductName,
                        ProductCategory = transaction.Product.Category.ToString(),
                        Quantity = transaction.Quantity,
                        Price = transaction.SellingPrice,
                        TotalPrice = transaction.Quantity * transaction.SellingPrice
                    };
                    salesOrder.SalesTransactionDetails.Add(salesTransaction);
                }

                model.SalesOrderDetails.Add(salesOrder);
            }

            return View(model);
        }
    }
}
