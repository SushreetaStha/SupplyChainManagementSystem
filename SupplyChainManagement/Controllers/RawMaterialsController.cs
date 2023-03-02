using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyChainManagement.Authorization;
using SupplyChainManagement.Data;
using SupplyChainManagement.Models;
using SupplyChainManagement.ViewModels.RawMaterials;

namespace SupplyChainManagement.Controllers
{
    [Authorize(Role.Admin)]
    public class RawMaterialsController : Controller
    {
        private readonly DatabaseContext _db;

        public RawMaterialsController(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var materials = await _db.RawMaterials.OrderBy(r => r.MaterialCode).ToListAsync();
            return View(materials);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RawMaterialCreateViewModel model)
        {
            var material = new RawMaterial
            {
                MaterialCode = model.MaterialCode,
                Date = model.Date,
                CostPrice = model.CostPrice,
                MaterialName = model.MaterialName,
                Quantity = model.Quantity,
            };
            await _db.RawMaterials.AddAsync(material);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var material = await _db.RawMaterials.FindAsync(id);

            if (material == null)
                return View("Index");

            var model = new RawMaterialCreateViewModel
            {
                Id = material.Id,
                MaterialCode = material.MaterialCode,
                MaterialName = material.MaterialName,
                Date = material.Date,
                Quantity = material.Quantity,
                CostPrice = material.CostPrice,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RawMaterialCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var material = await _db.RawMaterials.FindAsync(model.Id);
            if (material == null)
                return View(model);

            material.MaterialCode = model.MaterialCode;
            material.MaterialName = model.MaterialName;
            material.Date = model.Date;
            material.Quantity = model.Quantity;
            material.CostPrice = model.CostPrice;

            _db.RawMaterials.Update(material);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var material = await _db.RawMaterials.FindAsync(id);
            if (material == null)
                return Json(new { success = false });

            _db.RawMaterials.Remove(material);
            await _db.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}

