using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Models.ViewModels;
using ShiftsSchedule.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ShiftsSchedule.ViewComponents
{
    public class ShiftsDateList : ViewComponent
    {
        private readonly WorkersRepository _repoWkr;
        private readonly ProjectsRepository _repoPrj;
        private readonly ShiftsRepository _repoShft;

        public ShiftsDateList(WorkersRepository repoWrk, ProjectsRepository repoPrj, ShiftsRepository repoShft)
        {
            _repoWkr = repoWrk;
            _repoPrj = repoPrj;
            _repoShft = repoShft;
        }

        public IViewComponentResult Invoke(int? workerId, int? projectId, bool? extended, bool? all)
        {
            var shifts = GetShifts(workerId, projectId, all);

            if (extended == true)
                return View("Extended", shifts);
            return View(shifts);
        }
        private IEnumerable<ShiftsViewModel> GetShifts(int? workerId, int? projectId, bool? all)
        {
            if (workerId != null)

                return Mapper.Map<IEnumerable<ShiftsViewModel>>(_repoWkr.GetWorkerShifts((int)workerId));

            if (projectId != null)
            {
                var project = _repoPrj.FindBy(p => p.Id == projectId).FirstOrDefault();
                var shiftsViewModels = Mapper.Map<IEnumerable<ShiftsViewModel>>(project.Shifts.ToList());
                return InitializeShiftViewModels(shiftsViewModels);
            }

            if (all != true) return new List<ShiftsViewModel>();
            {
                var shiftsViewModels = Mapper.Map<IEnumerable<ShiftsViewModel>>(_repoShft.GetAll().ToList());
                return InitializeShiftViewModels(shiftsViewModels);
            }
        }

        private IEnumerable<ShiftsViewModel> InitializeShiftViewModels(IEnumerable<ShiftsViewModel> shiftsViewModels)
        {
            if (shiftsViewModels == null) return new List<ShiftsViewModel>();
            var viewModelsList = shiftsViewModels as IList<ShiftsViewModel> ?? shiftsViewModels.ToList();
            foreach (var shift in viewModelsList)
            {
                shift.Workers = _repoShft.WorkersAttendingShift(shift.Id).ToList();
                shift.Project = _repoPrj.ProjectByShiftId(shift.Id);
            }
            return viewModelsList;
        }
    }
}