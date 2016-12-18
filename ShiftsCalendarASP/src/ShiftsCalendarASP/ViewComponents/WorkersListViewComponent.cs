using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsCalendarASP.Models.Repository;
using ShiftsCalendarASP.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftsCalendarASP.ViewComponents
{
    public class WorkersListViewComponent : ViewComponent
    {
        private readonly WorkersRepository _repo;

        public WorkersListViewComponent(WorkersRepository repo)
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
