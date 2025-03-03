using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyPortolio.DAL.Context;
using MyPortolio.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// SMTP ayarlarını yapılandır
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
// EmailService'i kaydet
builder.Services.AddTransient<EmailService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyPortfolioContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    options.UseSqlServer(configuration.GetConnectionString("MyPortfolioConnection"));
});



// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/AdminAuth/Login";
        options.LogoutPath = "/AdminAuth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();