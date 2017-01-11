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

namespace ShiftsSchedule.Controllers
{
    [Route("Shifts")]
    public class ShiftsController : Controller
    {
        private ProjectsRepository _repository;
        private ILogger<ShiftsController> _logger;
        
        #region CRUD ACTIONS

        public ShiftsController(ProjectsRepository repository, ILogger<ShiftsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        //READ
        //[HttpGet("")]
        //public IActionResult Get(int projectId)
        //{
        //    try
        //    {
        //        var project = _repository.FindBy(p => p.Id == projectId).FirstOrDefault();
        //        return Ok(Mapper.Map<IEnumerable<ShiftsViewModel>>(project.Shifts.ToList()));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Failed to get all shifts for project: {ex.Message}");
        //        return Redirect("/error");
        //    }
        //}
        //CREATE
        [HttpPost]
        public async Task<IActionResult> Create(int projectId, [FromForm]ShiftsViewModel shift)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newShift = Mapper.Map<Shift>(shift);
                    _repository.AddShiftForProject(projectId, newShift);
                    if (await _repository.SaveChangesAsync())
                    {
                        return RedirectToAction("Edit","Projects");
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
        //DELETE (LOGICAL)
        [HttpGet("dismiss/{shiftId}")] 
        public async Task<IActionResult> Dismiss(int shiftId)
        {
            if (ModelState.IsValid)
            {
                _repository.DeleteShift(shiftId);
                if (await _repository.SaveChangesAsync())
                {
                    return RedirectToActionPermanent("Edit", "Projects");
                }
            }

            _logger.LogError($"Failed to delete shift with id {shiftId}");
            return Redirect("/error");
        }

        #endregion
    }
}
