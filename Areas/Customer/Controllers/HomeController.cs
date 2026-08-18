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
        public async Task<IActionResult> Index()
        {
            return View();
    }
    }
}
