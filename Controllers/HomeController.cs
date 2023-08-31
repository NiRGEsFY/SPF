using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPF.Data;
using SPF.Models;
using System.Diagnostics;

namespace SPF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            var item = _context.Items
                .OrderByDescending(o => o.Top);
            if (item == null)
            {
                return NotFound();
            }

            return View(item.Take(4));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}