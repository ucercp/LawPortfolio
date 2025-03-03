using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortolio.DAL.Context;
using MyPortolio.DAL.Entities;

namespace MyPortolio.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly MyPortfolioContext _context;

        public MessageController(MyPortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Inbox()
        {
            var values = _context.Messages.ToList();
            return View(values);
        }
        public IActionResult ChangeIsReadToTrue(int id)
        {
            var value = _context.Messages.Find(id);
            value.IsRead = true;
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }

        public IActionResult ChangeIsReadToFalse(int id)
        {
            var value = _context.Messages.Find(id);
            value.IsRead = false;
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            _context.Messages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("Inbox");
        }
        public IActionResult MessageDetail(int id)
        {
            var value = _context.Messages.Find(id);
            return View(value);
        }


        //// Create eylemi
        //[HttpPost]
        //public IActionResult Create(Message message)
        //{
        //    message.SendDate = DateTime.Now;
        //    message.IsRead = false; // Yeni mesaj okunmamış olarak işaretlensin
        //    context.Messages.Add(message);
        //    context.SaveChanges();
        //    return RedirectToAction("Inbox"); // Gönderimden sonra gelen kutusuna yönlendirelim
        //}


        [HttpPost]
        public IActionResult Create(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false; // Yeni mesaj okunmamış olarak işaretlensin
            _context.Messages.Add(message);
            _context.SaveChanges();

            // Başarılı mesajı ViewBag ile geçin
            ViewBag.SuccessMessage = "Mesajınız başarıyla gönderildi!";
            return RedirectToAction("Index", "Default"); // Aynı sayfayı döndür
        }

    }

}
