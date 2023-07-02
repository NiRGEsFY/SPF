using Microsoft.AspNetCore.Mvc;

namespace SPF.Controllers
{
    public class Contacts : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
