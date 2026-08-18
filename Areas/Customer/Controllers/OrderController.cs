using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastBite.Data;
using FastBite.Models;
using FastBite.Models.ViewModel;
using FastBite.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastBite.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            var cart = await _db.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            var orderDetails = await _db.OrderDetails.Include(o => o.MenuItem).Where(o => o.OrderId == id).ToListAsync();
            var model = new OrderViewModel { cart = cart, OrderDetailsList = orderDetails };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var uid = _userManager.GetUserId(User);
            var carts = await _db.Cart.Where(c => c.userId == uid).ToListAsync();
            var model = new List<OrderViewModel>();
            foreach (var cart in carts)
            {
                var orderDetails = await _db.OrderDetails.Include(o => o.MenuItem).Where(o => o.OrderId == cart.Id).ToListAsync();
                Restaurant restaurant = null;
                var firstMenuItem = orderDetails.FirstOrDefault()?.MenuItem;
                if (firstMenuItem != null)
                {
                    restaurant = await _db.Restaurant.FindAsync(firstMenuItem.RestaurantId);
                }
                model.Add(new OrderViewModel { cart = cart, OrderDetailsList = orderDetails, Restaurant = restaurant });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PendingRestaurantOrderDetails(int restid)
        {
            var restaurant = await _db.Restaurant.FindAsync(restid);
            if (restaurant == null)
            {
                return NotFound();
            }
            var menuItemIds = await _db.MenuItem.Where(m => m.RestaurantId == restid).Select(m => m.Id).ToListAsync();
            var orderIds = await _db.OrderDetails.Where(o => menuItemIds.Contains(o.MenuItemId)).Select(o => o.OrderId).Distinct().ToListAsync();
            var carts = await _db.Cart.Where(c => orderIds.Contains(c.Id) &&
                (c.orderStatus == StaticDefinitions.PendingConfirmation || c.orderStatus == StaticDefinitions.OrderConfirmed)).ToListAsync();
            var model = new List<OrderViewModel>();
            foreach (var cart in carts)
            {
                var orderDetails = await _db.OrderDetails.Include(o => o.MenuItem).Where(o => o.OrderId == cart.Id).ToListAsync();
                model.Add(new OrderViewModel { cart = cart, OrderDetailsList = orderDetails, Restaurant = restaurant });
            }
            ViewBag.RestaurantName = restaurant.RestaurantName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptOrder(int id, int restid)
        {
            var cart = await _db.Cart.FindAsync(id);
            if (cart != null)
            {
                cart.orderStatus = StaticDefinitions.OrderConfirmed;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("PendingRestaurantOrderDetails", new { restid });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderPrepared(int id, int restid)
        {
            var cart = await _db.Cart.FindAsync(id);
            if (cart != null)
            {
                cart.orderStatus = StaticDefinitions.orderReady;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("PendingRestaurantOrderDetails", new { restid });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelOrder(int id, int restid)
        {
            var cart = await _db.Cart.FindAsync(id);
            if (cart != null)
            {
                cart.orderStatus = StaticDefinitions.OrderCancelled;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("PendingRestaurantOrderDetails", new { restid });
        }
    }
}
