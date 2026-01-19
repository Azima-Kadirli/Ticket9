using Microsoft.AspNetCore.Mvc;

namespace Ticket9.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
