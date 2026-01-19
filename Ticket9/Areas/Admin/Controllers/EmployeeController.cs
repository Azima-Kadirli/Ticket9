using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ticket9.Context;
using Ticket9.Helper;
using Ticket9.Models;
using Ticket9.ViewModel.Employee;

namespace Ticket9.Areas.Admin.Controllers;
[Area("Admin")]

public class EmployeeController : Controller
{

    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly string _folderPath;

    public EmployeeController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
        _folderPath = Path.Combine(_env.WebRootPath, "images");
    }

    public async Task<IActionResult> Index()
    {
        await _sendCategoryWithViewBag();
        var employee = await _context.Employees.Select(e => new EmployeeGetVM()
        {
            Id = e.Id,
            FullName = e.FullName,
            Image = e.Image,
            CategoryName = e.Category.Name
        }).ToListAsync();
        return View(employee);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee is null)
            return NotFound();
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Create()
    {
        await _sendCategoryWithViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateVM vm)
    {
        await _sendCategoryWithViewBag();
        if (!ModelState.IsValid)
            return View(vm);
        var existCategory = await _context.Categories.AnyAsync(c => c.Id == vm.CategoryId);
        if (!existCategory)
        {
            ModelState.AddModelError("CategoryId", "This category is not found");
            return View(vm);
        }

        if (!vm.Image?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("Image", "Zehmet olmasa image tipli sekil yukleyin");
            return View(vm);
        }
        if (!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Seklin max olcusu 2mb ola biler");
            return View(vm);
        }

        string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);

        Employee employee = new()
        {
            FullName = vm.FullName,
            Image = uniqueFileName,
            CategoryId = vm.CategoryId
        };

       await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        await _sendCategoryWithViewBag();
        var employee = await _context.Employees.FindAsync(id);
        if (employee is null)
            return NotFound();
        EmployeeUpdateVM vm = new()
        {
            FullName = employee.FullName,
            CategoryId = employee.CategoryId
        };

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Update(EmployeeUpdateVM vm)
    {
        await _sendCategoryWithViewBag();
        if (!ModelState.IsValid)
            return View(vm);

        var category = await _context.Employees.AnyAsync(e => e.Id == vm.CategoryId);
        if (!category)
        {
            ModelState.AddModelError("CategoryId", "This category is not found");
            return View(vm);
        }
        if (!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "Sekilin olcusu max 2mb ola biler");
            return View(vm);
        }
        if (!vm.Image?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("Image", "Zehmet olmasa image tipli sekil yukleyin");
            return View(vm);
        }

        var existEmployee = await _context.Employees.FindAsync(vm.Id);
        if (existEmployee is null)
            return NotFound();

        existEmployee.FullName = vm.FullName;
        existEmployee.CategoryId = vm.CategoryId;
        if (vm.Image is not null)
        {
            string newImagePath = await vm.Image.FileUploadAsync(_folderPath);
            string oldImagePath = Path.Combine(_folderPath, existEmployee.Image);
            ExtensionMethod.DeleteFile(oldImagePath);
            existEmployee.Image = newImagePath;
        }
        _context.Employees.Update(existEmployee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));


    }


    private async Task _sendCategoryWithViewBag()
    {
        var category = await _context.Categories.Select(c => new SelectListItem()
        {
            Text=c.Name,
            Value=c.Id.ToString()
        }).ToListAsync();
        ViewBag.Categories = category;
    }

}
