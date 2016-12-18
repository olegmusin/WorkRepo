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
    public class ProjectsListViewComponent : ViewComponent
    {
        private readonly ProjectsRepository _repo;
        public ProjectsListViewComponent(ProjectsRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            var items = GetItems();
            return View(items);
        }

        private IEnumerable<ProjectsViewModel> GetItems()
        {
            return Mapper.Map<IEnumerable<ProjectsViewModel>>(_repo.GetAll());
        }
    }
}
