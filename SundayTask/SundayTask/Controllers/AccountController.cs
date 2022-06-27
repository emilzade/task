using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SundayTask.ViewModels;
using System.Threading.Tasks;

namespace SundayTask.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            AppUser newAppUser = new AppUser()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.UserName
            };
            IdentityResult result = await _userManager.CreateAsync(newAppUser,register.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(register);
                }
            }
            await _signInManager.SignInAsync(newAppUser, false);
            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }
    }
}
