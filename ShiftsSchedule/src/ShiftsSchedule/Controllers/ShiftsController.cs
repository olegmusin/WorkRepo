using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShiftsSchedule.Models;
using ShiftsSchedule.Models.Repository;
using ShiftsSchedule.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ShiftsSchedule.Controllers
{
    [Route("Shifts")]
    public class ShiftsController : Controller
    {
        private readonly ProjectsRepository _projectsRepository;
        private readonly ShiftsRepository _shiftsRepository;
        private readonly WorkersRepository _workersRepository;
        private readonly ILogger<ShiftsController> _logger;

        #region CRUD ACTIONS

        public ShiftsController(ProjectsRepository projectsRepository, 
            ShiftsRepository shiftsRepository, 
            WorkersRepository workersRepository, ILogger<ShiftsController> logger)
        {
            _projectsRepository = projectsRepository;
            _shiftsRepository = shiftsRepository;
            _workersRepository = workersRepository;
            _logger = logger;
        }
        //GET
        [Authorize]
        [HttpGet]
        public IActionResult Shifts(string userId)
        {
            try 
            {
                if(User.IsInRole("admins") || User.IsInRole("operators"))             
                return View();
                if (User.IsInRole("workers"))
                {
                    var currentWorker = _workersRepository.FindBy(w => w.UserId == userId).First();
                    var shifts = _shiftsRepository.GetAllForWorker(currentWorker.Id);
                    return View(Mapper.Map<IEnumerable<ShiftsViewModel>>(shifts.ToList()));
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all shifts for project: {ex.Message}");
                return Redirect("Home/error");
            }
        }
        //CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int projectId, [FromForm]ShiftsViewModel shift)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newShift = Mapper.Map<Shift>(shift);
                    _projectsRepository.AddShiftForProject(projectId, newShift);
                    if (await _projectsRepository.SaveChangesAsync())
                    {
                        return Redirect($"Projects/Edit/{projectId}");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add new shift {shift.Date.Date} for project {projectId} due to: {ex.Message}");
                return Redirect("/error");
            }

            return BadRequest($"Failed to add new shift {shift.Date.Date} for project {projectId}");
        }

        #endregion

        public IActionResult SignUpToShift()
        {
            throw new NotImplementedException();
        }
    }
}
