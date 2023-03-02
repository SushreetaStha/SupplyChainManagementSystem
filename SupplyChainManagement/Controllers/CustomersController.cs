using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Authorization;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;
using SupplyChainManagement.ViewModels.Customers;

namespace SupplyChainManagement.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly DatabaseContext _db;

        public CustomersController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var customers = _db.Customers.OrderBy(c => c.CustomerCode).ToList();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateViewModel model)
        {
            var customer = new Customer
            {
                CustomerCode = model.CustomerCode,
                Date = model.Date,
                CustomerName = model.CustomerName,
                Contact = model.Contact,
                Address = model.Address,
            };
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var customer = await _db.Customers.FindAsync(id);

            if (customer == null)
                return View("Index");

            var model = new CustomerCreateViewModel
            {
                Id = customer.Id,
                CustomerCode = customer.CustomerCode,
                CustomerName = customer.CustomerName,
                Date = customer.Date,
                Contact = customer.Contact,
                Address = customer.Address,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var customer = await _db.Customers.FindAsync(model.Id);
            if (customer == null)
                return View(model);

            customer.CustomerCode = model.CustomerCode;
            customer.CustomerName = model.CustomerName;
            customer.Date = model.Date;
            customer.Contact = model.Contact;
            customer.Address = model.Address;

            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null)
                return Json(new { success = false });

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
