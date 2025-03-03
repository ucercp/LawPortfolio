using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortolio.DAL.Context;
using MyPortolio.DAL.Entities;

namespace MyPortolio.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        private readonly MyPortfolioContext _context;

        public AboutController(MyPortfolioContext context)
        {
            _context = context;
        }


        public IActionResult AboutList() //hakkımdaki blgileri getir,listele
        {
            var values = _context.Abouts.ToList();
            return View(values);
        }

        ////hakkımda ile ilgili yeni bir bilgi ekle
        //[HttpGet]
        //public IActionResult CreateAbout()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult CreateAbout(About about)
        //{  
        //    context.Abouts.Add(about);
        //    context.SaveChanges();
        //    return RedirectToAction("AboutList");
        //}


        //Güncelleme işlemi yap
        [HttpGet]
        public IActionResult UpdateAbout(int id)
        {
            var values = _context.Abouts.Find(id);//once güncelleyeceğin ürünü bul
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateAbout(About about)
        {
            _context.Abouts.Update(about);//httpget kısmında buldugun ürünü güncelle
            _context.SaveChanges();
            return RedirectToAction("AboutList");

        }
    }
}
//uygulama şifresi{ hhuk qywb rmba czcv }