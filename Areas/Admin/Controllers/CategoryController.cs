

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FastBite.Data;
using FastBite.Models;
using Microsoft.AspNetCore.Authorization;
using FastBite.Utility;

namespace FastBite.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = StaticDefinitions.Admin)]
     public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _db.Category.OrderBy(c => c.Id).ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                var existing = await _db.Category.FindAsync(id);
                if (existing == null)
                {
                    return NotFound();
                }
                existing.Name = category.Name;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var existing = await _db.Category.FindAsync(id);
            if (existing != null)
            {
                _db.Category.Remove(existing);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
