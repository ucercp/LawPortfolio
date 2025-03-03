using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortolio.DAL.Context;
using MyPortolio.Models;


namespace MyPortolio.Controllers
{
    public class PasswordResetController : Controller
    {
        private readonly MyPortfolioContext _context;
        private readonly EmailService _emailService;

        public PasswordResetController(MyPortfolioContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Şifre sıfırlama isteği formu
        [HttpGet]
        public IActionResult RequestReset()
        {
            return View();
        }

        // Şifre sıfırlama isteği işleme (POST)
        [HttpPost]
        public async Task<IActionResult> RequestReset(string email)
        {
            var user = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Bu e-posta adresiyle kayıtlı bir kullanıcı bulunamadı.");
                return View();
            }

            // Token oluştur ve kaydet
            var token = GeneratePasswordResetToken();
            user.PasswordResetToken = token;
            user.ResetTokenExpires = DateTime.UtcNow.AddHours(1); // Token 1 saat geçerli
            _context.AdminUsers.Update(user);
            await _context.SaveChangesAsync();

            // E-posta gönder
            var resetLink = Url.Action("ResetPassword", "PasswordReset", new { token }, Request.Scheme);
            var subject = "Şifre Sıfırlama İsteği";
            var emailBody = $"Şifrenizi sıfırlamak için <a href='{resetLink}'>bu linke</a> tıklayın.";

            try
            {
                await _emailService.SendEmailAsync(email, subject, emailBody);
                ViewBag.Message = "Şifre sıfırlama linki e-posta adresinize gönderildi.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "E-posta gönderilirken bir hata oluştu: " + ex.Message);
            }

            return View();
        }

        // Yeni şifre belirleme formu
        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            var user = _context.AdminUsers.FirstOrDefault(x => x.PasswordResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);
            if (user == null)
            {
                var errorModel = new ErrorViewModel
                {
                    ErrorMessage = "Geçersiz veya süresi dolmuş token."
                };
                return View("Error", errorModel); // Error.cshtml sayfasına yönlendir
            }

            return View();
        }

        // Yeni şifre belirleme işleme (POST)
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string token, string newPassword)
        {
            var user = await _context.AdminUsers.FirstOrDefaultAsync(u => u.PasswordResetToken == token && u.ResetTokenExpires > DateTime.UtcNow);
            if (user == null)
            {
                ViewBag.Error = "Geçersiz veya süresi dolmuş token.";
                return View("Error");
            }

            // Yeni şifreyi hash'le ve kaydet
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetToken = null; // Token'i temizle
            user.ResetTokenExpires = null; // Süreyi temizle
            _context.AdminUsers.Update(user);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Şifreniz başarıyla güncellendi.";
            return RedirectToAction("Login", "AdminAuth"); // Kullanıcıyı login sayfasına yönlendir
        }

        // Token oluşturma
        private string GeneratePasswordResetToken()
        {
            return Guid.NewGuid().ToString(); // Rastgele bir GUID oluştur
        }
    }
}