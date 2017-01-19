using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Models.ViewModels;
using ShiftsSchedule.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ShiftsSchedule.Models;

namespace ShiftsSchedule.ViewComponents
{
    public class ShiftsDateList : ViewComponent
    {
        private readonly WorkersRepository _repoWkr;
        private ProjectsRepository _repoPrj;
        private ShiftsRepository _repoShft;

        public ShiftsDateList(WorkersRepository repoWrk, ProjectsRepository repoPrj, ShiftsRepository repoShft)
        {
            _repoWkr = repoWrk;
            _repoPrj = repoPrj;
            _repoShft = repoShft;
        }

        public IViewComponentResult Invoke(int? workerId, int? projectId, bool? extended)
        {
            var shifts = GetShifts(workerId, projectId);

            if (extended == true)
                return View("Extended", shifts);
            return View(shifts);
        }
        private IEnumerable<ShiftsViewModel> GetShifts(int? workerId, int? projectId)
        {
            if (workerId != null)
            {
                return Mapper.Map<IEnumerable<ShiftsViewModel>>(_repoWkr.GetWorkerShifts((int)workerId));
            }
            else if (projectId != null)
            {
                var project = _repoPrj.FindBy(p => p.Id == projectId).FirstOrDefault();
                var shifts = Mapper.Map<IEnumerable<ShiftsViewModel>>((project.Shifts.ToList()));

                foreach(var shift in shifts)
                {
                    shift.Workers = _repoShft.WorkersAttendingShift(shift.Id).ToList();
                }

                return shifts; 
            }
            else return new List<ShiftsViewModel>();
        }

    }
}