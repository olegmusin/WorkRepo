using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShiftsCalendar.Models;
using ShiftsCalendar.Models.Repository;
using ShiftsCalendar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftsCalendar.Controllers.Api
{
    [Route("api/workers")]
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
        public IActionResult Get()
        {
            try
            {
                var workers = _repository.GetAll();
                return Ok(Mapper.Map<IEnumerable<WorkersViewModel>>(workers));
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
        public async Task<IActionResult> Post([FromBody]WorkersViewModel worker)
        {
            if (ModelState.IsValid)
            {
                var newWorker = Mapper.Map<Worker>(worker);
                _repository.Add(newWorker);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/workers/{newWorker.Name}", Mapper.Map<WorkersViewModel>(newWorker));
                }
            }
            _logger.LogError($"Failed to add new worker {worker.Name}");
            return BadRequest();
        }

        //UPDATE
        [HttpPut("edit/{workerId}")]
        public async Task<IActionResult> Edit([FromBody]WorkersViewModel worker)
        {
            if (ModelState.IsValid)
            {
                var editWorker = Mapper.Map<Worker>(worker);
                _repository.Edit(editWorker);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(Mapper.Map<WorkersViewModel>(editWorker));
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
                var delWorker = _repository.GetSingle(workerId);
                _repository.Delete(delWorker);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }

            _logger.LogError($"Failed to delete worker with id {workerId}");
            return BadRequest();
        }

        //GETSHIFTSBYID
        [HttpGet("{workerId}/shifts")]
        public IActionResult GetShiftsById(int workerId)
        {
            try
            {
                var shifts = _repository.GetWorkerShifts(workerId);
                return Ok(Mapper.Map<IEnumerable<ShiftsViewModel>>(shifts.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get shifts for worker with Id: {workerId} from database, due to => {ex.Message}");
                return BadRequest();
            }

        }

    }

}
