using Microsoft.AspNetCore.Identity;
using SPF.Entities;
using System.Security.Claims;

namespace SPF
{
    public class DataBaseInitialization
    {
        public static async void Initialize(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();
            var password = "1234qwer";
            var userName = "admin";
            var adminClaim = new Claim(ClaimTypes.Role, "Administrator");
            var moderClaim = new Claim(ClaimTypes.Role, "Moder");
            var user = userManager.FindByNameAsync(userName).GetAwaiter().GetResult();
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userName,
                    FirstName = "admin",
                    LastName = "tester",
                    PostUser = "Server_Administrator"
                };
                var result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddClaimAsync(user, adminClaim).GetAwaiter().GetResult();
                }
            }
            else
            {

                userManager.ReplaceClaimAsync(user, adminClaim, moderClaim);
                userManager.UpdateAsync(user);
            }
        }
    }
}
