using System;
using System.Linq;
using System.Threading.Tasks;
using FastBite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FastBite.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using FastBite.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FastBite.Areas.Admin.Controllers
{
     [Area("Admin")]
     [Authorize(Roles = StaticDefinitions.RestaurantOwner)]
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public RestaurantController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _db = db;
            _userManager = userManager;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var uid = _userManager.GetUserId(User);
            var restaurants = await _db.Restaurant.Where(r => r.OwenerID == uid).OrderBy(r => r.Id).ToListAsync();
            return View(restaurants);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Restaurant());
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
        public async Task<IActionResult> Create(Restaurant restaurant, IFormFile imageurl)
        {
            if (ModelState.IsValid)
            {
                restaurant.OwenerID = _userManager.GetUserId(User);
                if (imageurl != null && imageurl.Length > 0)
                {
                    restaurant.imageurl = await SaveImage(imageurl, "restaurant");
                }
                else
                {
                    restaurant.imageurl = "/images/" + StaticDefinitions.defaultimage;
                }
                _db.Restaurant.Add(restaurant);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await _db.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Restaurant restaurant, IFormFile imageurl)
        {
            var existing = await _db.Restaurant.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                existing.RestaurantName = restaurant.RestaurantName;
                existing.Address = restaurant.Address;
                if (imageurl != null && imageurl.Length > 0)
                {
                    existing.imageurl = await SaveImage(imageurl, "restaurant");
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var uid = _userManager.GetUserId(User);
            var restaurant = await _db.Restaurant.FindAsync(id);
            if (restaurant != null)
            {
                if (restaurant.OwenerID != uid)
                {
                    return Forbid();
                }
                _db.Restaurant.Remove(restaurant);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }

}
