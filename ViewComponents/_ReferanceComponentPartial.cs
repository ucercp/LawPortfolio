using Microsoft.AspNetCore.Mvc;
using MyPortolio.DAL.Context;

namespace MyPortolio.ViewComponents
{
    public class _ReferanceComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext _context;

        public _ReferanceComponentPartial(MyPortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Referances.ToList();
            return View(values);
        }
    }
}
