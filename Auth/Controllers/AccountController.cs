using Auth.Models;
using Auth.ViewModels;
using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;

        private readonly UserManager<AppUser> userManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            var result = await this.signInManager.PasswordSignInAsync(model.Login, model.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("NotFound", "Home");
        }

        #endregion

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser(
                    model.Email,
                    model.Password,
                    RoleType.CommonUser, 
                    model.Login, 
                    model.Login,
                    model.Login.ToUpper());

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.PasswordSignInAsync(model.Login, model.Password, true, lockoutOnFailure: false);

                    return RedirectToAction("Index", "Home");
                }

                return View(model);
            }
            return View(model);
        }

        #endregion
    }
}
