using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Route("Workers")]
    [AutoValidateAntiforgeryToken]
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
        [HttpGet]
        [Authorize]
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
        
        //ADD
        [HttpPost]
        public async Task<IActionResult> Add(WorkersViewModel worker)
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
        [HttpPost("edit/{workerId}")]
        public async Task<IActionResult> Update(WorkersViewModel worker)
        {
            if (ModelState.IsValid)
            {
                var editWorker = Mapper.Map<Worker>(worker);
                _repository.Edit(editWorker);
                if (await _repository.SaveChangesAsync())
                {
                    return RedirectToAction("Workers");
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
        //Navigate to edit page
        [HttpGet("edit/{workerId}")]
        public IActionResult Edit(int workerId)
        {
            try
            {
                var editWorker = _repository.GetSingle(workerId);
                return View("EditWorker", Mapper.Map<WorkersViewModel>(editWorker));
            }
            catch
            {
                _logger.LogError($"Can't fetch worker {workerId} for edit!");
                return RedirectToAction("/Home/Error");
            }
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
