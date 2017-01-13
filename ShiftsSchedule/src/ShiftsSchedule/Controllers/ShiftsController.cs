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
        //DELETE (LOGICAL)
        [HttpGet("dismiss/{shiftId}")] 
        public async Task<IActionResult> Dismiss(int shiftId)
        {
            if (ModelState.IsValid)
            {
                var projectId = _repository.ProjectByShiftId(shiftId).Id;
                _repository.DeleteShift(shiftId);
                if (await _repository.SaveChangesAsync())
                { 
                    return RedirectToRoute("Api", projectId);
                }
            }

            _logger.LogError($"Failed to delete shift with id {shiftId}");
            return Redirect("/error");
        }

        #endregion
    }
}
