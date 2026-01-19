using Microsoft.AspNetCore.Mvc;

namespace Ticket9.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
