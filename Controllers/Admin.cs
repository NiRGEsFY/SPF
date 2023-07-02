using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.Security.Claims;

namespace SPF.Controllers
{
    [Authorize]
    public class Admin : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }
        [Authorize(Roles = "Moder")]
        public ActionResult Moder()
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Administrator()
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var claims = new List<Claim>
            {
                new Claim("Demo", "Value"),
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.Role, "Administrator")
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);

            return Redirect(model.ReturnURL);
        }
        public ActionResult LogOff()
        {
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Index");
        }

        public class LoginViewModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            public string ReturnURL { get; set; }

        }
    }
}
