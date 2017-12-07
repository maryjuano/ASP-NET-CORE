using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Stark.DAL;
using Stark.Presentation.ViewModels;

namespace Stark.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<StarkIdentityUser> _signInManager;
        private readonly UserManager<StarkIdentityUser> _userManager;
        public AuthController(SignInManager<StarkIdentityUser> signInManager,
                              UserManager<StarkIdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("UserName,Password,RememberMe")] LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var result = await this._signInManager.PasswordSignInAsync(
               viewModel.UserName, viewModel.Password,
               isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
                return Redirect(returnUrl ?? "~/Home/");


            ModelState.AddModelError(string.Empty, "Login Failed.");
            return View(viewModel);
        }


        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}