using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyPortolio.DAL.Context;
using System.Security.Claims;

namespace MyPortolio.Controllers
{
    public class AdminAuthController : Controller
    {
        private readonly MyPortfolioContext _context;

        public AdminAuthController(MyPortfolioContext context)
        {
            _context = context;
        }

        // Login GET metodunu zaten yazdınız
        public IActionResult Login()
        {
            return View();
        }

        // Login POST metodunu zaten yazdınız
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var admin = _context.AdminUsers.FirstOrDefault(x => x.Username == username);

            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim("Admin", "true")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("AboutList", "About"); // Admin paneline yönlendir
        }

        // Admin panelinden çıkış işlemi (Logout)
        public async Task<IActionResult> Logout()
        {
            // Kullanıcıyı çıkartıyoruz
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["Message"] = "Admin panelinden çıkış yapıldı!";

            // Login sayfasına yönlendiriyoruz
            return RedirectToAction("Login", "AdminAuth");
        }
    }
}
