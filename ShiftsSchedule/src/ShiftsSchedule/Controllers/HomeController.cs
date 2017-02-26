using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShiftsSchedule.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Shifts Schedule";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Developer Contacts";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
