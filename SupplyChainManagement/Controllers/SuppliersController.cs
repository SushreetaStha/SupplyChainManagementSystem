using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Authorization;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;
using SupplyChainManagement.ViewModels.Suppliers;

namespace SupplyChainManagement.Controllers
{
    [Authorize]
    public class SuppliersController : Controller
    {
        private readonly DatabaseContext _db;

        public SuppliersController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var suppliers = _db.Suppliers.OrderBy(c => c.SupplierCode).ToList();
            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateViewModel model)
        {
            var supplier = new Supplier
            {
                SupplierCode = model.SupplierCode,
                Date = model.Date,
                SupplierName = model.SupplierName,
                Contact = model.Contact,
                Address = model.Address,
            };
            await _db.Suppliers.AddAsync(supplier);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await _db.Suppliers.FindAsync(id);

            if (supplier == null)
                return View("Index");

            var model = new SupplierCreateViewModel
            {
                Id = supplier.Id,
                SupplierCode = supplier.SupplierCode,
                SupplierName = supplier.SupplierName,
                Date = supplier.Date,
                Contact = supplier.Contact,
                Address = supplier.Address,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SupplierCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var supplier = await _db.Suppliers.FindAsync(model.Id);
            if (supplier == null)
                return View(model);

            supplier.SupplierCode = model.SupplierCode;
            supplier.SupplierName = model.SupplierName;
            supplier.Date = model.Date;
            supplier.Contact = model.Contact;
            supplier.Address = model.Address;

            _db.Suppliers.Update(supplier);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await _db.Suppliers.FindAsync(id);
            if (supplier == null)
                return Json(new { success = false });

            _db.Suppliers.Remove(supplier);
            await _db.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
