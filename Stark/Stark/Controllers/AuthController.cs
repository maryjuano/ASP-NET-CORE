using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stark.Models;

namespace Stark.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("UserName,Password,RememberMe")] LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                return Redirect(returnUrl ?? "~/Home/");
            }
            ModelState.AddModelError("", "Invalid username or password.");
            return View(viewModel);
        }
    }
}
