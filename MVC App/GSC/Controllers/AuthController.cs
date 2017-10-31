using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GSC.Controllers
{
    public class AuthController : Controller
    {
        public AuthController() {

        }
        
        public IActionResult Index()
        {         
          return View();
        }

        [HttpPost]
        public IActionResult Auth()
        {
            //TODO: Implement Realistic Implementation
        }
    }
}
