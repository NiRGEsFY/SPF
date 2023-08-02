using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using SPF.Entities;

namespace SPF.Controllers
{
    [Authorize]
    public class Admin : Controller
    {
        // GET: Admin
        [Authorize]
        public class AdminController : Controller
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;

            public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }
            public async Task<IActionResult> Index()
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.Login = user.UserName;
                ViewBag.Post = user.PostUser;


                return View();
            }
            [Authorize(Policy = "Administrator")]
            public IActionResult Admin()
            {
                return View();
            }
            [Authorize(Policy = "Moder")]
            public IActionResult Moder()
            {
                return View();
            }

            [AllowAnonymous]
            public IActionResult Login(string returnUrl)
            {
                return View();
            }
            [HttpPost]
            [AllowAnonymous]
            public async Task<IActionResult> Login(LoginViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("", "User Not Found");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl);
                }
                return View(model);


            }

            public async Task<IActionResult> LogOff()
            {
                await _signInManager.SignOutAsync();
                return Redirect("/Home/Index");
            }
            public IActionResult AccessDenied()
            {
                return View();
            }
        }
        public class LoginViewModel
        {
            [Required]
            public string UserName { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string ReturnUrl { get; set; }
        }
    }
}
