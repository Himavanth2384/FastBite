using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using FastBite.Data;
using FastBite.Models;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FastBite.Models.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using FastBite.Utility;

//using System.Web.Mvc;

namespace FastBite.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = StaticDefinitions.Admin)]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var subCategories = await _db.SubCategory.Include(s => s.category).OrderBy(s => s.Id).ToListAsync();
            return View(subCategories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CategoryAndSubCategoryModel
            {
                categoryList = await _db.Category.OrderBy(c => c.Id).ToListAsync(),
                subCategoryList = await _db.SubCategory.OrderBy(s => s.Id).Select(s => s.Name).ToListAsync(),
                subCategory = new SubCategory()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryAndSubCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                _db.SubCategory.Add(model.subCategory);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            model.categoryList = await _db.Category.OrderBy(c => c.Id).ToListAsync();
            model.subCategoryList = await _db.SubCategory.OrderBy(s => s.Id).Select(s => s.Name).ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var subCategory = await _db.SubCategory.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            var model = new CategoryAndSubCategoryModel
            {
                categoryList = await _db.Category.OrderBy(c => c.Id).ToListAsync(),
                subCategoryList = await _db.SubCategory.OrderBy(s => s.Id).Select(s => s.Name).ToListAsync(),
                subCategory = subCategory
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryAndSubCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                model.subCategory.Id = id;
                _db.SubCategory.Update(model.subCategory);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            model.categoryList = await _db.Category.OrderBy(c => c.Id).ToListAsync();
            model.subCategoryList = await _db.SubCategory.OrderBy(s => s.Id).Select(s => s.Name).ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var subCategory = await _db.SubCategory.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return View(subCategory);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var existing = await _db.SubCategory.FindAsync(id);
            if (existing != null)
            {
                _db.SubCategory.Remove(existing);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
