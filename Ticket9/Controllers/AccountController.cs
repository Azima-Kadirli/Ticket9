using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ticket9.Models;
using Ticket9.ViewModel.Account;

namespace Ticket9.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signManager = signManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            AppUser user = new()
            {
                Email = vm.Email,
                FullName = vm.FullName,
                UserName = vm.UserName
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            await _userManager.AddToRoleAsync(user, "User");
            await _signManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if(user is null)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(vm);
            }
            var result = await _signManager.PasswordSignInAsync(user, vm.Password, false, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(vm);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "Admin"
        //    });
        //    await _roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "User"
        //    });
        //    return Ok("Create roles");
        //}
    }
}
