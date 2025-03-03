using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyPortolio.DAL.Context;

namespace MyPortolio.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyPortfolioContext _context;

        public AdminController(MyPortfolioContext context)
        {
            _context = context;
        }

        // Şifre ve kullanıcı adı değiştirme formunu gösterecek GET metodu
        public IActionResult ChangePassword()
        {
            return View();
        }

        // Şifreyi değiştirecek POST metodu
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string newPassword, string username)
        {
            // Admin kullanıcısını buluyoruz
            var adminUser = _context.AdminUsers.FirstOrDefault(u => u.AdminUserId == 1);
            if (adminUser != null)
            {
                // Şifreyi hash'liyoruz
                adminUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                // Kullanıcı adını değiştiriyoruz
                if (!string.IsNullOrEmpty(username))
                {
                    adminUser.Username = username;
                }

                _context.SaveChanges(); // Veritabanına kaydediyoruz

                // Kimlik bilgilerini güncelliyoruz
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, adminUser.Username),
                    new Claim("Admin", "true")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                // Kimlik bilgilerini güncelliyoruz
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                TempData["Message"] = "Şifre ve/veya kullanıcı adı başarıyla değiştirildi!";
                return RedirectToAction("Login", "AdminAuth"); // Anasayfaya yönlendiriyoruz
            }

            TempData["Error"] = "Admin kullanıcısı bulunamadı!";
            return View();
        }

        // Admin panelinden çıkış işlemi (logout)
        public async Task<IActionResult> Logout()
        {
            // Kullanıcıyı çıkartıyoruz
            await HttpContext.SignOutAsync();

            TempData["Message"] = "Admin panelinden çıkış yapıldı!";

            // Login sayfasına yönlendiriyoruz
            return RedirectToAction("Login", "AdminAuth");  // Login sayfasına yönlendiriyoruz
        }
    }
}




// #region
// public IActionResult Login()
// {
//     return View();
// }
// [HttpPost]
// public IActionResult Login(string username, string password)
// {
//     string adminUsername = "RcpUce";
//     string adminPasswor = "123456aA*";

//     if (username == adminUsername && password == adminPasswor)
//     {
//         var cookie = new CookieOptions
//         {
//             Expires = DateTime.Now.AddMinutes(30),
//             HttpOnly = true,
//             Secure = true
//         };
//         Response.Cookies.Append("AdminAuth", "true", cookie);

//         return RedirectToAction("AboutList", "About");
//     }

//     else
//     {

//         ViewBag.Error = "Giriş bilgileriniz hatalı veya yetkiniz yok";
//         return View();
//     }

// }

//public IActionResult Logout() 
// {
//     Response.Cookies.Delete("AdminAuth");

//     return RedirectToAction("Login");
// }
// #endregion