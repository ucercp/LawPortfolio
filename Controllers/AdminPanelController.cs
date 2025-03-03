using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyPortolio.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            if (!Request.Cookies.ContainsKey("AdminAuth"))
            {
                return RedirectToAction("Login", "AdminAuth");
            }
            return View();
        }
    }
}
