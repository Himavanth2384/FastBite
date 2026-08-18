using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastBite.Data;
using FastBite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastBite.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartItemController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AddTocart(int id)
        {
            var menuItem = await _db.MenuItem.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            var cartItem = new CartItem
            {
                MenuItemId = id,
                MenuItem = menuItem,
                Count = 1
            };
            return View(cartItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTocart(int id, int Count)
        {
            var menuItem = await _db.MenuItem.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            if (Count < 1)
            {
                Count = 1;
            }
            var uid = _userManager.GetUserId(User);
            var existingItems = await _db.CartItem.Where(c => c.ApplicationUserId == uid).ToListAsync();
            if (existingItems.Any())
            {
                var existingMenuItemIds = existingItems.Select(c => c.MenuItemId).ToList();
                var existingRestaurantId = await _db.MenuItem.Where(m => existingMenuItemIds.Contains(m.Id))
                    .Select(m => m.RestaurantId).FirstOrDefaultAsync();
                if (existingRestaurantId != menuItem.RestaurantId)
                {
                    _db.CartItem.RemoveRange(existingItems);
                    await _db.SaveChangesAsync();
                    existingItems.Clear();
                }
            }
            var existing = existingItems.FirstOrDefault(c => c.MenuItemId == id);
            if (existing != null)
            {
                existing.Count += Count;
                _db.CartItem.Update(existing);
            }
            else
            {
                _db.CartItem.Add(new CartItem { ApplicationUserId = uid, MenuItemId = id, Count = Count });
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("MenuItems", "Home", new { id = menuItem.RestaurantId });
        }
    }
}
