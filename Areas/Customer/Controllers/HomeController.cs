using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FastBite.Models;
using FastBite.Data;
using Microsoft.EntityFrameworkCore;
using FastBite.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace FastBite.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var restaurants = await _db.Restaurant.OrderBy(r => r.Id).ToListAsync();
            return View(restaurants);
        }

        public async Task<IActionResult> MenuItems(int id)
        {
            var restaurant = await _db.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            var menuItems = await _db.MenuItem.Include(m => m.Category).Where(m => m.RestaurantId == id)
                .OrderBy(m => m.CategoryId).ThenBy(m => m.Id).ToListAsync();
            var categories = menuItems.GroupBy(m => m.CategoryId).Select(g => g.First().Category).ToList();
            var model = new CategoryAndMenuItemViewModel
            {
                Restaurant = restaurant,
                MenuItem = menuItems,
                Category = categories
            };
            return View(model);
        }
    }
}
