using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastBite.Data;
using FastBite.Models;
using FastBite.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FastBite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDefinitions.Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleLock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (await _userManager.IsLockedOutAsync(user))
                {
                    await _userManager.SetLockoutEndDateAsync(user, null);
                }
                else
                {
                    await _userManager.SetLockoutEnabledAsync(user, true);
                    await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
                }
            }
            return RedirectToAction("Index");
        }

        public class NewEmployeeInputModel
        {
            [Required]
            public string Name { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }

        [HttpGet]
        public IActionResult NewEmployeeRegistration()
        {
            return View(new NewEmployeeInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewEmployeeRegistration(NewEmployeeInputModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = input.Email,
                    Email = input.Email,
                    Name = input.Name,
                    Address = input.Address,
                    PhoneNumber = input.PhoneNumber,
                    EmailConfirmed = true,
                    LockoutEnabled = true
                };
                var result = await _userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                {
                    var roleName = Request.Form["rdUserRole"].ToString();
                    if (string.IsNullOrEmpty(roleName))
                    {
                        roleName = StaticDefinitions.Customer;
                    }
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                    await _userManager.AddToRoleAsync(user, roleName);
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(input);
        }
    }
}
