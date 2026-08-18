using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FastBite.Data;
using FastBite.Models;
using FastBite.Models.ViewModel;
using FastBite.Models.Viewmodels;
using FastBite.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FastBite.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = StaticDefinitions.RestaurantOwner)]
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public MenuItemController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var restaurant = await _db.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            var menuItems = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory)
                .Where(m => m.RestaurantId == id).ToListAsync();
            var model = new RestaurantMenuItemViewModel
            {
                Restaurant = restaurant,
                MenuItemList = menuItems
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var model = new MenuItemViewModel
            {
                MenuItem = new MenuItem { RestaurantId = id, price = 0 },
                Category = await _db.Category.ToListAsync(),
                SubCategory = await _db.SubCategory.ToListAsync()
            };
            return View(model);
        }

        private async Task<string> SaveImage(IFormFile file, string folder)
        {
            var dir = Path.Combine(_env.WebRootPath, "images", folder);
            Directory.CreateDirectory(dir);
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(dir, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/images/" + folder + "/" + fileName;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemViewModel model, IFormFile MenuItem_image)
        {
            if (ModelState.IsValid)
            {
                if (MenuItem_image != null && MenuItem_image.Length > 0)
                {
                    model.MenuItem.imageUrl = await SaveImage(MenuItem_image, "menuitems");
                }
                else
                {
                    model.MenuItem.imageUrl = "/images/" + StaticDefinitions.defaultimage;
                }
                _db.MenuItem.Add(model.MenuItem);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = model.MenuItem.RestaurantId });
            }
            model.Category = await _db.Category.ToListAsync();
            model.SubCategory = await _db.SubCategory.ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var menuItem = await _db.MenuItem.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            var model = new MenuItemViewModel
            {
                MenuItem = menuItem,
                Category = await _db.Category.ToListAsync(),
                SubCategory = await _db.SubCategory.ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItemViewModel model, IFormFile MenuItem_image)
        {
            var existing = await _db.MenuItem.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                existing.Name = model.MenuItem.Name;
                existing.description = model.MenuItem.description;
                existing.price = model.MenuItem.price;
                existing.CategoryId = model.MenuItem.CategoryId;
                existing.SubCategoryId = model.MenuItem.SubCategoryId;
                if (MenuItem_image != null && MenuItem_image.Length > 0)
                {
                    existing.imageUrl = await SaveImage(MenuItem_image, "menuitems");
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = existing.RestaurantId });
            }
            model.Category = await _db.Category.ToListAsync();
            model.SubCategory = await _db.SubCategory.ToListAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var menuItem = await _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).FirstOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }
            var model = new MenuItemViewModel { MenuItem = menuItem };
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var existing = await _db.MenuItem.FindAsync(id);
            var restaurantId = 0;
            if (existing != null)
            {
                restaurantId = existing.RestaurantId;
                _db.MenuItem.Remove(existing);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = restaurantId });
        }
    }
}
