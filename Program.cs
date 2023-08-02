using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SPF.Data;
using SPF.Entities;
using System.Security.Claims;

namespace SPF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(connectionString)).AddIdentity<ApplicationUser, ApplicationRole>(config =>
                    {
                        config.Password.RequireDigit = false;
                        config.Password.RequireLowercase = false;
                        config.Password.RequireUppercase = false;
                        config.Password.RequireNonAlphanumeric = false;
                        config.Password.RequiredLength = 8;
                    })
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Admin/Login";
                config.AccessDeniedPath = "/Admin/AccessDenied";
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });

                options.AddPolicy("Moder", builder =>
                {
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Administrator") ||
                                                  x.User.HasClaim(ClaimTypes.Role, "Moder"));
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}