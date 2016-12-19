using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsCalendarASP.Models.Repository;
using ShiftsCalendarASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftsCalendarASP.ViewComponents
{
    public class AddEditProjectViewComponent: ViewComponent
    {

        private readonly ProjectsRepository _repo;

        public AddEditProjectViewComponent(ProjectsRepository repo)
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
                return View();
        }

        private ProjectsViewModel GetProject(int projectId)
        {
            return Mapper.Map<ProjectsViewModel>(_repo.GetSingle(projectId));
        }
    }
}

