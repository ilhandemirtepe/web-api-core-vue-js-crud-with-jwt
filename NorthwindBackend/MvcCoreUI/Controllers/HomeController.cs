using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcCoreUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreUI.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult IndexOne()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

    }
}
