using Microsoft.AspNetCore.Mvc;
using ShiftsCalendarASP.ViewModels;
using ShiftsCalendarASP.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ShiftsCalendarASP.ViewComponents
{
    public class ShiftsDateListViewComponent : ViewComponent
    {
        private readonly WorkersRepository _repoWkr;
        private ProjectsRepository _repoPrj;

        public ShiftsDateListViewComponent(WorkersRepository repoWrk, ProjectsRepository repoPrj)
        {
            _repoWkr = repoWrk;
            _repoPrj = repoPrj;
        }

        public IViewComponentResult Invoke(int? workerId, int? projectId)
        {
            var items = GetItems(workerId, projectId);
            return View(items);
        }
        private IEnumerable<ShiftsViewModel> GetItems(int? workerId, int? projectId)
        {
            if (workerId != null)
            {
                return Mapper.Map<IEnumerable<ShiftsViewModel>>(_repoWkr.GetWorkerShifts((int)workerId));
            }
            else if (projectId != null)
            {
                var project = _repoPrj.FindBy(p => p.Id == projectId).FirstOrDefault();
                return Mapper.Map<IEnumerable<ShiftsViewModel>>((project.Shifts.ToList()));
            }
            else return new List<ShiftsViewModel>();
        }

    }
}