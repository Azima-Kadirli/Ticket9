using Microsoft.AspNetCore.Mvc;
using Ticket9.Context;

namespace Ticket9.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly string _folderPath;
    public HomeController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
        _folderPath = Path.Combine(_env.WebRootPath,"images");
    }

    public IActionResult Index()
    {
        return View();
    }
}
