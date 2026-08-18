using System.Linq;
using System.Threading.Tasks;
using FastBite.Data;
using FastBite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FastBite.Utility;

namespace FastBite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDefinitions.Admin)]
    public class OfferController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OfferController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var offers = await _db.Offer.OrderBy(o => o.Id).ToListAsync();
            return View(offers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Offer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Offer offer)
        {
            if (ModelState.IsValid)
            {
                _db.Offer.Add(offer);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(offer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _db.Offer.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            return View(offer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Offer offer)
        {
            if (ModelState.IsValid)
            {
                offer.Id = id;
                _db.Offer.Update(offer);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(offer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _db.Offer.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            return View(offer);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var existing = await _db.Offer.FindAsync(id);
            if (existing != null)
            {
                _db.Offer.Remove(existing);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
