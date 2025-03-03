using Microsoft.AspNetCore.Mvc;
using MyPortolio.DAL.Context;
using MyPortolio.DAL.Entities;
using System;
using System.Linq;

namespace MyPortolio.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly MyPortfolioContext _context;

        public TestimonialController(MyPortfolioContext context)
        {
            _context = context;
        }

        // Tüm yorumları listeleme (Admin)
        public IActionResult TestimonialList()
        {
            var values = _context.Testimonials.ToList();
            return View(values);
        }

        // Yorum ekleme sayfası (Admin)
        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial testimonial)
        {
            testimonial.CreatedAt = DateTime.Now; // Yorum oluşturulduğunda tarih otomatik ayarlanır
            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        // Kullanıcıların yorum yapması için yeni bir metot
        [HttpPost]
        public IActionResult AddTestimonial(Testimonial testimonial)
        {
            testimonial.CreatedAt = DateTime.Now; // Kullanıcının tarihi girmesine izin verme, sistem otomatik versin
            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
            return RedirectToAction("Index", "Default"); // Kullanıcı yorum yaptıktan sonra anasayfaya yönlendir
        }

        // Yorum silme (Admin)
        public IActionResult DeleteTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            if (value != null)
            {
                _context.Testimonials.Remove(value);
                _context.SaveChanges();
            }
            return RedirectToAction("TestimonialList");
        }

        // Güncelleme işlemleri (Admin)
        [HttpGet]
        public IActionResult UpdateTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateTestimonial(Testimonial testimonial)
        {
            var existingTestimonial = _context.Testimonials.Find(testimonial.TestimonialId);
            if (existingTestimonial != null)
            {
                existingTestimonial.Title = testimonial.Title;
                existingTestimonial.NameSurname = testimonial.NameSurname;
                existingTestimonial.Description = testimonial.Description;
                existingTestimonial.CreatedAt = DateTime.Now; // Güncelleme anındaki tarih atanıyor

                _context.SaveChanges();
                return RedirectToAction("TestimonialList");
            }
            return View(testimonial);
        }
    }
}
