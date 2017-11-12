using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stark.Models;
using Microsoft.AspNetCore.Identity;
using Stark.Data.Identity;

namespace Stark.Controllers
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

        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUser(NewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new StarkIdentityUser {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    FullName = $"{ model.FirstName ?? string.Empty } { model.MiddleName?.Substring(0) ?? string.Empty } { model.LastName }"
                };
               
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                  
                    return RedirectToAction(nameof(NewUser));
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
