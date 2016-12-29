using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Models.Repository;
using ShiftsSchedule.Models.ViewModels;

namespace ShiftsSchedule.ViewComponents
{
    public class AddEditProject: ViewComponent
    {

        private readonly ProjectsRepository _repo;

        public AddEditProject(ProjectsRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke(int? projectId)
        {
            if (projectId != null)
            {
                var project = GetProject((int)projectId);
                return View(project);
            }
            else
                return View(new ProjectsViewModel());
        }

        private ProjectsViewModel GetProject(int projectId)
        {
            return Mapper.Map<ProjectsViewModel>(_repo.GetSingle(projectId));
        }
    }
}

