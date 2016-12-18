﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShiftsCalendarASP.Models;
using ShiftsCalendarASP.Models.Repository;
using ShiftsCalendarASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftsCalendarASP.Controllers.Api
{
    [Route("api/projects/{projectId}/shifts")]
    public class ShiftsController : Controller
    {
        private ProjectsRepository _repository;
        private ILogger<ShiftsController> _logger;
        private ShiftsViewModel _vm;

        public ShiftsController(ProjectsRepository repository, ILogger<ShiftsController> logger, ShiftsViewModel vm)
        {
            _repository = repository;
            _logger = logger;
            _vm = vm;
        }
        //GET
        [HttpGet("")]
        public IActionResult Get(int projectId)
        {
            try
            {
                var project = _repository.FindBy(p => p.Id == projectId).FirstOrDefault();
                return Ok(Mapper.Map<IEnumerable<ShiftsViewModel>>(project.Shifts.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all shifts for project: {ex.Message}");
                return Redirect("/error");
            }
        }
        //POST
        [HttpPost("")]
        public async Task<IActionResult> Post(int projectId, [FromBody]ShiftsViewModel shift)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newShift = Mapper.Map<Shift>(shift);
                    _repository.AddShiftForProject(projectId, newShift);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"api/projects/{projectId}/shifts/{newShift.Id}",
                               Mapper.Map<ShiftsViewModel>(newShift));
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
        //DELETE
        [HttpDelete("{shiftId}/delete")]
        public async Task<IActionResult> Delete(int shiftId)
        {
            if (ModelState.IsValid)
            {
                _repository.DeleteShift(shiftId);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }

            _logger.LogError($"Failed to delete shift with id {shiftId}");
            return Redirect("/error");
        }

    }
}