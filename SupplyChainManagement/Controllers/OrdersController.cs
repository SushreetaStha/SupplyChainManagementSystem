using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupplyChainManagement.Authorization;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;
using SupplyChainManagement.ViewModels.Orders;

namespace SupplyChainManagement.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly DatabaseContext _db;

        public OrdersController(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Transactions)
                    .ThenInclude(t => t.Product)
                .Where(o => !o.IsCompleted)
                .ToListAsync();

            var model = new OrderIndexViewModel();
            model.OrderDetails = new List<OrderIndexDetails>();

            foreach (var order in orders)
            {
                var orderDetail = new OrderIndexDetails();
                orderDetail.OrderId = order.Id;
                orderDetail.OrderDate = order.Date;
                orderDetail.OrderCode = order.OrderCode;
                orderDetail.CustomerName = order.Customer.CustomerName;

                var isItemsInStock = IsOrderItemsInStock(order.Transactions);
                orderDetail.IsItemInStock = isItemsInStock;

                model.OrderDetails.Add(orderDetail);
            }

            model.OrderDetails = model.OrderDetails.OrderBy(o => o.OrderCode).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new OrderCreateViewModel();
            var customers = await _db.Customers.ToListAsync();
            var products = await _db.Products.ToListAsync();
            var customersList = customers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.CustomerName });
            var productItems = products.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.ProductName });
            model.CustomersList = customersList;
            model.ProductsList = productItems;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            var transactions = new List<Transaction>();
            var orderCode = form["OrderCode"];
            var customerId = Guid.Parse(form["CustomerId"]);

            var products = form.Where(x => x.Key.Contains("product")).ToList();
            var quantities = form.Where(x => x.Key.Contains("quantity")).ToList();

            for (var j = 0; j < products.Count; j++)
            {
                var transaction = new Transaction
                {
                    ProductId = Guid.Parse(products[j].Value),
                    Quantity = int.Parse(quantities[j].Value)
                };
                var product = await _db.Products.FindAsync(transaction.ProductId);
                transaction.SellingPrice = product.SellingPrice;
                transactions.Add(transaction);
            }

            var grandTotal = transactions.Sum(t => t.Quantity * t.SellingPrice);

            var order = new Order
            {
                OrderCode = orderCode,
                CustomerId = customerId,
                Date = DateTime.Now,
                IsCompleted = false,
                Transactions = transactions,
                GrandTotal = grandTotal
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> OrderItems(Guid id)
        {
            var model = new OrderItemsViewModel();
            model.OrderItems = new List<OrderItem>();

            var order = await _db.Orders
                .Include(c => c.Customer)
                .Include(c => c.Transactions)
                    .ThenInclude(t => t.Product)
                .SingleOrDefaultAsync(c => c.Id == id);

            model.CustomerName = order.Customer.CustomerName;
            model.GrandTotal = order.GrandTotal;

            foreach (var trn in order.Transactions)
            {
                var orderItem = new OrderItem();
                orderItem.OrderQuantity = trn.Quantity;
                orderItem.SellingPrice = trn.SellingPrice;
                orderItem.ProductName = trn.Product.ProductName;
                orderItem.OrderPending = trn.Product.Quantity - trn.Quantity;
                model.OrderItems.Add(orderItem);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Ship(Guid id)
        {
            using var dbTransaction = _db.Database.BeginTransaction();

            var order = await _db.Orders
            .Include(o => o.Transactions)
                .ThenInclude(t => t.Product)
            .SingleOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return Json(new { success = false });

            if (!IsOrderItemsInStock(order.Transactions))
                return Json(new { success = false, inStock = false });

            order.IsCompleted = true;
            _db.Orders.Update(order);

            foreach (var trn in order.Transactions)
            {
                var product = await _db.Products.FindAsync(trn.ProductId);

                if (product == null)
                    return Json(new { success = false });

                product.Quantity = product.Quantity - trn.Quantity;
                _db.Products.Update(product);
            }

            await _db.SaveChangesAsync();
            dbTransaction.Commit();

            return Json(new { success = true });
        }

        public async Task<IActionResult> ShippedOrders()
        {
            var orders = await _db.Orders
                .Include(o => o.Customer)
                .Where(o => o.IsCompleted)
                .ToListAsync();

            var model = new OrderIndexViewModel();
            model.OrderDetails = new List<OrderIndexDetails>();

            foreach (var order in orders)
            {
                var orderDetail = new OrderIndexDetails();
                orderDetail.OrderId = order.Id;
                orderDetail.OrderDate = order.Date;
                orderDetail.OrderCode = order.OrderCode;
                orderDetail.CustomerName = order.Customer.CustomerName;

                model.OrderDetails.Add(orderDetail);
            }

            model.OrderDetails = model.OrderDetails.OrderBy(o => o.OrderCode).ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShippedOrderItems(Guid id)
        {
            var model = new OrderItemsViewModel();
            model.OrderItems = new List<OrderItem>();

            var order = await _db.Orders
                .Include(c => c.Customer)
                .Include(c => c.Transactions)
                    .ThenInclude(t => t.Product)
                .SingleOrDefaultAsync(c => c.Id == id);

            model.CustomerName = order.Customer.CustomerName;
            model.GrandTotal = order.GrandTotal;

            foreach (var trn in order.Transactions)
            {
                var orderItem = new OrderItem();
                orderItem.OrderQuantity = trn.Quantity;
                orderItem.SellingPrice = trn.SellingPrice;
                orderItem.ProductName = trn.Product.ProductName;
                model.OrderItems.Add(orderItem);
            }

            return View(model);
        }

        private bool IsOrderItemsInStock(IEnumerable<Transaction> transactions)
        {
            var isEverythingInStock = false;
            foreach (var trn in transactions)
            {
                if (trn.Quantity <= trn.Product.Quantity)
                {
                    isEverythingInStock = true;
                }
                else
                {
                    isEverythingInStock = false;
                    break;
                }
            }

            return isEverythingInStock;
        }
    }
}
