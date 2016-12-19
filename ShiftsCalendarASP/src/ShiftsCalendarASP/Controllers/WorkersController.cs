using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShiftsCalendarASP.Models;
using ShiftsCalendarASP.Models.Repository;
using ShiftsCalendarASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftsCalendarASP.Controllers
{
    [Route("Workers")]
    public class WorkersController : Controller
    {
        private ILogger<WorkersController> _logger;
        private WorkersRepository _repository;
        

        public WorkersController(WorkersRepository repository, ILogger<WorkersController> logger)
        {
            _repository = repository;
            _logger = logger;
   
        }
        //GET
        [HttpGet("")]
        public IActionResult Workers()
        {
            try
            {
                var workers = _repository.GetAll();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all workers from database, due to => {ex.Message}");
                return BadRequest();
            }

        }
        //GETBYID
        [HttpGet("{workerId}")]
        public IActionResult GetById(int workerId)
        {
            try
            {
                var worker = _repository.GetSingle(workerId);
                return Ok(Mapper.Map<WorkersViewModel>(worker));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get worker with Id: {workerId} from database, due to => {ex.Message}");
                return BadRequest();
            }

        }
        
        //POST
        [HttpPost("")]
        public async Task<IActionResult> AddWorker([FromForm]WorkersViewModel worker)
        {
            if (ModelState.IsValid)
            {
                var newWorker = Mapper.Map<Worker>(worker);
                _repository.Add(newWorker);
                if (await _repository.SaveChangesAsync())
                {
                    return View("Workers");
                }
            }
            _logger.LogError($"Failed to add new worker {worker.Name}");
            return BadRequest();
        }

        //UPDATE
        [HttpGet("edit/{workerId}")]
        public async Task<IActionResult> Edit([FromForm]WorkersViewModel worker)
        {
            if (ModelState.IsValid)
            {
                var editWorker = Mapper.Map<Worker>(worker);
                _repository.Edit(editWorker);
                if (await _repository.SaveChangesAsync())
                {
                    return View("Workers", Mapper.Map<WorkersViewModel>(editWorker));
                }
            }

            _logger.LogError($"Failed to edit worker with Id: {worker.Id}");
            return BadRequest();
        }
        //DELETE
        [HttpGet("delete/{workerId}")]
        public async Task<IActionResult> Delete(int workerId)
        {
            if (ModelState.IsValid)
            {            
                _repository.Delete(workerId);
                if (await _repository.SaveChangesAsync())
                {
                    return RedirectToAction("Workers");
                }
            }

            _logger.LogError($"Failed to delete worker with id {workerId}");
            return Redirect("/Home/error");
        }

        //GETSHIFTSBYID
        [HttpGet("{workerId}/shifts")]
        public IActionResult GetShiftsById(int workerId)
        {
            try
            {
                var shifts = _repository.GetWorkerShifts(workerId);
                return ViewComponent("ShiftsList",Mapper.Map<IEnumerable<ShiftsViewModel>>(shifts.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get shifts for worker with Id: {workerId} from database, due to => {ex.Message}");
                return BadRequest();
            }

        }

    }

}
