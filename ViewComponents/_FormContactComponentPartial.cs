using Microsoft.AspNetCore.Mvc;
using MyPortolio.DAL.Context;
using MyPortolio.DAL.Entities;

namespace MyPortolio.ViewComponents
{
    public class _FormContactComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var message = new Message();
            return View("Default");


        }
    }
}
