using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SitePustok.Models;
using SitePustok.ViewModels.AuthVM;

namespace SitePustok.Contollers
{
    public class AuthController : Controller
    {
        //SignInManager<AppUser> _signInManager { get; }
        UserManager<AppUser> _userManager { get; }

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
            var result = await _userManager.CreateAsync(new AppUser
            {
                Fullname = vm.Fullname,
                Email = vm.Email,
            }, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }
            return View();
        }
    }
}
