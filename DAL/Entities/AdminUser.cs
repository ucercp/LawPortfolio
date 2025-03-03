namespace MyPortolio.DAL.Entities
{
    public class AdminUser
    {
        public int AdminUserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public string? PasswordResetToken { get; set; } // Şifre sıfırlama token'ı
        public DateTime? ResetTokenExpires { get; set; } // Token'ın geçerlilik süresi


    }
}
