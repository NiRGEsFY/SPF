using Microsoft.AspNetCore.Mvc;

namespace SPF.Controllers
{
    public class Catalog : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
