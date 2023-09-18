using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SPF.Data;
using SPF.Entities;
using SPF.Models;


namespace SPF.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class UserEditController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserEditController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Index(string role)
        {
            var users = _userManager.Users;
            if (role == "admin")
                users = _userManager.Users.Take(20).Where(o => o.PostUser == "Директор" || o.PostUser == "Система");
            else
                users = _userManager.Users.Take(20).Where(o => o.PostUser != "Директор" && o.PostUser != "Система");
            return View(users);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreate _user)
        {
            ModelState.Remove("Role");
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = _user.UserName,
                    FirstName = _user.FirstName,
                    LastName = _user.LastName,
                    PostUser = _user.PostUser,
                };
                var result = _userManager.CreateAsync(user, _user.Password).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    if (_user.Role == "admin")
                        _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
                    else
                        _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Moder")).GetAwaiter().GetResult();
                }
                return RedirectToAction("Index", "UserEdit");
            }
            else
                return View(_user);

        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser origUser = _userManager.Users.FirstOrDefault(o => o.Id == id);
            if (origUser == null)
            {
                return NotFound();
            }
            UserCreate user = new UserCreate
            {
                UserName = origUser.UserName,
                FirstName = origUser.FirstName,
                LastName = origUser.LastName,
                PostUser = origUser.PostUser,
                Password = ""
            };
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, UserCreate newUser)
        {
            if (id == null)
            {
                return NotFound();
            }
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Users.FirstOrDefault(o => o.Id == id);
                if (newUser.Password == "" || newUser.Password == null)
                {
                    user.UserName = newUser.UserName;
                    user.FirstName = newUser.FirstName;
                    user.LastName = newUser.LastName;
                    user.PostUser = newUser.PostUser;
                    _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                    return RedirectToAction("Index");
                }
                if (user != null && newUser.Password != "")
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<ApplicationUser>)) as IPasswordValidator<ApplicationUser>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<ApplicationUser>)) as IPasswordHasher<ApplicationUser>;

                    var result =
                        _passwordValidator.ValidateAsync(_userManager, user, newUser.Password).GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        user.UserName = newUser.UserName;
                        user.FirstName = newUser.FirstName;
                        user.LastName = newUser.LastName;
                        user.PostUser = newUser.PostUser;
                        user.PasswordHash = _passwordHasher.HashPassword(user, newUser.Password);
                        _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Пользователь не найден");
                }
            }
            return View(newUser);
        }

        public ActionResult Delete(Guid id)
        {
            if (id == null || _userManager.Users == null)
            {
                return NotFound();
            }
            var user = _userManager.Users
                .FirstOrDefault(o => o.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (_userManager.Users == null)
            {
                return Problem("Identity set 'userManager.User' is null.");
            }
            var user = _userManager.Users
                .FirstOrDefault(o => o.Id == id);
            if (user != null)
            {
                var result = _userManager.DeleteAsync(user).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","UserEdit");
                }
                return Problem("Identity set 'userManager.User' cant delete user");
            }
            else
            {
                return NotFound();
            }
        }
    }
    
}
