using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [AutoValidateAntiforgeryToken]
    public class ProjectsController : Controller
    {
        private readonly ProjectsRepository _repository;
        private readonly ILogger<ProjectsController> _logger;
     


        public ProjectsController(ProjectsRepository repository, ILogger<ProjectsController> logger)
        {
            _repository = repository;
            _logger = logger;
       
        }

        #region CRUD Actions
        //GET
        [HttpGet]
        [Authorize]
        public IActionResult Projects()
        {           
          return View();           
        }

        //ADD
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ProjectsViewModel project)
        {
            if (ModelState.IsValid)
            {
                var newProject = Mapper.Map<Project>(project);
                _repository.Add(newProject);
                if (await _repository.SaveChangesAsync())
                {
                    return RedirectToAction("Projects");
                }
            }
            _logger.LogError($"Failed to add new project {project.Name}");
            return BadRequest();
        }

        //UPDATE
        [HttpPost("edit/{projectId}")]
        [Authorize]
        public async Task<IActionResult> Update(ProjectsViewModel project)
        {
            if (ModelState.IsValid)
            {
                var editProject = Mapper.Map<Project>(project);
                _repository.Edit(editProject);
                if (await _repository.SaveChangesAsync())
                {
                    return RedirectToAction("Projects");
                }
            }

            _logger.LogError($"Failed to edit project");
            return BadRequest();
        }

        //DELETE
        [HttpGet("delete/{projectId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int projectId)
        {
            if (ModelState.IsValid)
            {
                _repository.Delete(projectId);
                if (await _repository.SaveChangesAsync())
                {
                    _logger.LogInformation($"Project with Id {projectId} has been marked as deleted");
                    return Ok();
                }
            }

            _logger.LogError($"Failed to delete project with id {projectId}");
            return Redirect("/Home/error");
        }

        #endregion

        #region Navigate Actions
        //Navigate to Edit page
        [HttpGet("edit/{projectId}")]
        public IActionResult Edit(int projectId)
        {
            try
            {
                var editProject = _repository.GetSingle(projectId);
                return View("EditProject", Mapper.Map<ProjectsViewModel>(editProject));
            }
            catch
            {
                _logger.LogError($"Can't fetch project {projectId} for edit!");
                return RedirectToAction("Error","Home");
            }
        }


        #endregion

        #region Miscellanious Actions

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

        #endregion
    }
}
