using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Models.Repository;
using ShiftsSchedule.Models.ViewModels;
using System.Collections.Generic;

namespace ShiftsSchedule.ViewComponents
{
    public class WorkersList : ViewComponent
    {
        private readonly WorkersRepository _repo;

        public WorkersList(WorkersRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            var items = GetItems();
            return View(items);
        }
        private IEnumerable<WorkersViewModel> GetItems()
        {
            return Mapper.Map<IEnumerable<WorkersViewModel>>(_repo.GetAll());
        }
    }
}
