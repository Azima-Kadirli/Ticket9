using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ticket9.Context;
using Ticket9.ViewModel.Employee;

namespace Ticket9.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
