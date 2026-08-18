using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastBite.Data;
using FastBite.Models;
using FastBite.Models.ViewModel;
using FastBite.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastBite.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        private async Task<CartViewModel> BuildCartViewModel()
        {
            var uid = _userManager.GetUserId(User);
            var currentUser = await _userManager.GetUserAsync(User);
            var cartItems = await _db.CartItem.Where(c => c.ApplicationUserId == uid).ToListAsync();
            foreach (var ci in cartItems)
            {
                ci.MenuItem = await _db.MenuItem.FindAsync(ci.MenuItemId);
            }
            var amountWithoutDiscount = cartItems.Sum(c => c.MenuItem.price * c.Count);
            var offercode = HttpContext.Session.GetString(StaticDefinitions.CouponCode);
            Offer offer = null;
            if (!string.IsNullOrEmpty(offercode))
            {
                offer = await _db.Offer.FirstOrDefaultAsync(o => o.Name == offercode && o.isActive);
            }
            var discountedTotal = StaticDefinitions.DiscountPrice(offer, amountWithoutDiscount);
            var cart = new Cart
            {
                name = currentUser.Name,
                mobilenumber = currentUser.PhoneNumber,
                Address = currentUser.Address,
                AmountWithoutDiscount = amountWithoutDiscount,
                offercode = offercode,
                discount = Math.Round(amountWithoutDiscount - discountedTotal, 2),
                OrderTotal = discountedTotal
            };
            return new CartViewModel { CartItemList = cartItems, Cart = cart };
        }

        [HttpGet]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = await BuildCartViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(Cart cart)
        {
            var uid = _userManager.GetUserId(User);
            var currentUser = await _userManager.GetUserAsync(User);
            var cartItems = await _db.CartItem.Where(c => c.ApplicationUserId == uid).ToListAsync();
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Home");
            }
            foreach (var ci in cartItems)
            {
                ci.MenuItem = await _db.MenuItem.FindAsync(ci.MenuItemId);
            }
            var amountWithoutDiscount = cartItems.Sum(c => c.MenuItem.price * c.Count);
            var offercode = HttpContext.Session.GetString(StaticDefinitions.CouponCode);
            Offer offer = null;
            if (!string.IsNullOrEmpty(offercode))
            {
                offer = await _db.Offer.FirstOrDefaultAsync(o => o.Name == offercode && o.isActive);
            }
            var discountedTotal = StaticDefinitions.DiscountPrice(offer, amountWithoutDiscount);

            var order = new Cart
            {
                userId = uid,
                email = currentUser.Email,
                name = cart.name,
                mobilenumber = cart.mobilenumber,
                Address = cart.Address,
                AmountWithoutDiscount = amountWithoutDiscount,
                offercode = offercode,
                discount = Math.Round(amountWithoutDiscount - discountedTotal, 2),
                OrderTotal = discountedTotal,
                orderStatus = StaticDefinitions.PendingConfirmation
            };
            _db.Cart.Add(order);
            await _db.SaveChangesAsync();

            foreach (var ci in cartItems)
            {
                _db.OrderDetails.Add(new OrderDetails
                {
                    OrderId = order.Id,
                    MenuItemId = ci.MenuItemId,
                    Count = ci.Count,
                    Name = ci.MenuItem.Name,
                    Description = ci.MenuItem.description
                });
            }
            _db.CartItem.RemoveRange(cartItems);
            await _db.SaveChangesAsync();
            HttpContext.Session.Remove(StaticDefinitions.CouponCode);

            return RedirectToAction("ConfirmOrder", "Order", new { id = order.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IncrementCartItem(int id)
        {
            var item = await _db.CartItem.FindAsync(id);
            if (item != null)
            {
                item.Count += 1;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("PlaceOrder");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DecrementCartItem(int id)
        {
            var item = await _db.CartItem.FindAsync(id);
            if (item != null)
            {
                if (item.Count > 1)
                {
                    item.Count -= 1;
                    await _db.SaveChangesAsync();
                }
            }
            return RedirectToAction("PlaceOrder");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCartItem(int id)
        {
            var item = await _db.CartItem.FindAsync(id);
            if (item != null)
            {
                _db.CartItem.Remove(item);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("PlaceOrder");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyOffer(string offercode)
        {
            var offer = await _db.Offer.FirstOrDefaultAsync(o => o.Name == offercode && o.isActive);
            if (offer != null)
            {
                HttpContext.Session.SetString(StaticDefinitions.CouponCode, offercode);
            }
            return RedirectToAction("PlaceOrder");
        }
    }
}
