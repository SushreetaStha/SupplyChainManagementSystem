using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyChainManagement.Authorization;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;
using SupplyChainManagement.ViewModels.Home;
using System.Diagnostics;

namespace SupplyChainManagement.Controllers
{
    [Authorize(Role.Admin, Role.User)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _db;

        public HomeController(ILogger<HomeController> logger, DatabaseContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            DateTime currentDate = DateTime.Today;
            int year = currentDate.Year;

            var model = new DashboardViewModel();
            model.TotalOrders = _db.Orders.Count();
            model.TotalCustomers = _db.Customers.Count();
            model.TotalSuppliers = _db.Suppliers.Count();

            var income = _db.Orders.Where(o => o.Date.Year == year).Sum(o => o.GrandTotal);
            var expense = 0d;
            model.Profit = income - expense;

            var products = await _db.Products.ToListAsync();
            model.ProductPieChart.ProductNames = products.Select(p => p.ProductName).ToList();
            model.ProductPieChart.ProductQuantities = products.Select(p => p.Quantity).ToList();

            var orders = await _db.Orders.Where(o => o.Date.Year == year).ToListAsync();

            for (int i = 1; i <= 12; i++)
            {
                var count = orders.Count(o => o.Date.Month == i);
                model.OrderBarChart.OrdersCount.Add(count);
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}