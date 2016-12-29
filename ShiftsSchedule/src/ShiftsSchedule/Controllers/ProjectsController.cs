using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShiftsSchedule.Models;
using ShiftsSchedule.Models.Repository;
using ShiftsSchedule.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftsSchedule.Controllers
{
    [Route("Projects")]
    public class ProjectsController : Controller
    {
        private ProjectsRepository _repository;
        private ILogger<ProjectsController> _logger;
     


        public ProjectsController(ProjectsRepository repository, ILogger<ProjectsController> logger)
        {
            _repository = repository;
            _logger = logger;
       
        }

        //GET
        [HttpGet("")]
        public IActionResult Projects()
        {
            try
            {
                var projects = _repository.GetAll();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all projects from database, due to => {ex.Message}");
                return BadRequest();
            }

        }

        //GETBYID
        [HttpGet("{projectId}")]
        public IActionResult GetById(int projectId)
    {
        try
        {
            var project = _repository.GetSingle(projectId);
            return Ok(Mapper.Map<ProjectsViewModel>(project));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get project {projectId} from database, due to => {ex.Message}");
                return BadRequest();
            }

    }
        //GETBYNAME
        [HttpGet("projectName")]
        public IActionResult GetByName(string projectName)
        {
            try
            {
                var project = _repository.FindBy( p => p.Name == projectName).Take(0);
                return Ok(Mapper.Map<ProjectsViewModel>(project));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get project {projectName} from database, due to => {ex.Message}");
                return BadRequest();
            }

        }

        //POST
        [HttpPost("")]
        public async Task<IActionResult> AddProject([FromForm]ProjectsViewModel project)
        {
            if (ModelState.IsValid)
            {
                var newProject = Mapper.Map<Project>(project);
                _repository.Add(newProject);
                if (await _repository.SaveChangesAsync())
                {
                    return View("Projects");
                }
            }
            _logger.LogError($"Failed to add new project {project.Name}");
            return BadRequest();
        }

        //UPDATE
        [HttpPut("edit/{projectId}")]
        public async Task<IActionResult> Edit([FromBody]ProjectsViewModel project)
        {
            if (ModelState.IsValid)
            {
                var editProject = Mapper.Map<Project>(project);
                _repository.Edit(editProject);
                if (await _repository.SaveChangesAsync())
                {
                    return View("Projects");
                }
            }

            _logger.LogError($"Failed to edit project");
            return BadRequest();
        }

        //DELETE
        [HttpGet("delete/{projectId}")]
        public async Task<IActionResult> Delete(int projectId)
        {
            if (ModelState.IsValid)
            {            
                _repository.Delete(projectId);
                if (await _repository.SaveChangesAsync())
                {
                    return RedirectToAction("Projects");
                }
            }

            _logger.LogError($"Failed to delete project with id {projectId}");
            return Redirect("/Home/error");
        }




    }
}
