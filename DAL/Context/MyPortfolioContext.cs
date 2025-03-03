using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyPortolio.DAL.Entities;
using System.IO;

namespace MyPortolio.DAL.Context
{
    public class MyPortfolioContext : DbContext
    {
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Referance> Referances { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }


        private readonly IConfiguration _configuration;
        public MyPortfolioContext(DbContextOptions<MyPortfolioContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("MyPortfolioConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // İlk Admin Kullanıcıyı veritabanına ekliyoruz, şifreyi hash'liyoruz
            var adminUser = new AdminUser
            {
                AdminUserId = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), // İlk şifreyi hash'liyoruz
                Email = "rcp.uce92@gmail.com",
                PasswordResetToken = null, // Başlangıçta null
                ResetTokenExpires = null // Başlangıçta null
            };
            modelBuilder.Entity<AdminUser>().HasData(adminUser); // Veritabanına ekliyoruz




        }
    }
}
