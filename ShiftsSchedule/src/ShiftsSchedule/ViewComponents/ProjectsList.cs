﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Models.Repository;
using ShiftsSchedule.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShiftsSchedule.ViewComponents
{
    public class ProjectsList : ViewComponent
    {
        private readonly ProjectsRepository _repo;
        public ProjectsList(ProjectsRepository repo)
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
