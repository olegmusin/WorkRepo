using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShiftsCalendar.ViewModels;
using ShiftsCalendar.Models.Repository;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ShiftsCalendar.Models;

namespace ShiftsCalendar.Controllers.Web
{
    public class AppController : Controller
    {
        
        private ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            
            _logger = logger;    
        }
        [HttpGet]
        public IActionResult Index()
        {

            _logger.LogInformation(200, HttpContext.Connection.RemoteIpAddress.ToString());
            return Ok();

        }

      
    }
}
