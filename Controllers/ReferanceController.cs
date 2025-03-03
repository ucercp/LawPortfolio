using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortolio.DAL.Context;
using MyPortolio.DAL.Entities;

namespace MyPortolio.Controllers
{
    [Authorize]
    public class ReferanceController : Controller
    {
        private readonly MyPortfolioContext _context;

        public ReferanceController(MyPortfolioContext context)
        {
            _context = context;
        }
        public IActionResult ReferanceList()
        {
            var values = _context.Referances.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateReferance()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateReferance(Referance referance)
        {
            _context.Referances.Add(referance);
            _context.SaveChanges();
            return RedirectToAction("ReferanceList");
        }

        public IActionResult DeleteReferance(int id)
        {
            var value = _context.Referances.Find(id);
            _context.Referances.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("ReferanceList");
        }

        [HttpGet]
        public IActionResult UpdateReferance(int id)
        {
            var value = _context.Referances.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateReferance(Referance referance)
        {
            _context.Referances.Update(referance);
            _context.SaveChanges();
            return RedirectToAction("ReferanceList");
        }
    }


}
