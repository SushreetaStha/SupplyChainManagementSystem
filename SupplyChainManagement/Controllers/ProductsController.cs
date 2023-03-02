using Microsoft.AspNetCore.Mvc;
using SupplyChainManagement.Authorization;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;
using SupplyChainManagement.ViewModels.Products;

namespace SupplyChainManagement.Controllers
{
    [Authorize(Role.Admin)]
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _db;

        public ProductsController(DatabaseContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _db.Products.OrderBy(p => p.ProductCode).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var product = new Product
            {
                ProductCode = model.ProductCode,
                Date = model.Date,
                Category = model.Category,
                ProductName = model.ProductName,
                Quantity = model.Quantity,
                SellingPrice = model.SellingPrice,
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _db.Products.FindAsync(id);

            if (product == null)
                return View("Index");

            var model = new ProductCreateViewModel
            {
                Id = product.Id,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Date = product.Date,
                Quantity = product.Quantity,
                Category = product.Category,
                SellingPrice = product.SellingPrice,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var product = await _db.Products.FindAsync(model.Id);
            if (product == null)
                return View(model);

            product.ProductCode = model.ProductCode;
            product.ProductName = model.ProductName;
            product.Date = model.Date;
            product.Quantity = model.Quantity;
            product.Category = model.Category;
            product.SellingPrice = model.SellingPrice;

            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
                return Json(new { success = false });

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
