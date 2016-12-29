using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Models.Repository;
using ShiftsSchedule.Models.ViewModels;

namespace ShiftsSchedule.ViewComponents
{
    public class AddEditWorker: ViewComponent
    {

        private readonly WorkersRepository _repo;

        public AddEditWorker(WorkersRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke(int? workerId)
        {
            if (workerId != null)
            {
                var worker = GetWorker((int)workerId);
                return View(worker);
            }
            else
                return View(new WorkersViewModel());
        }

        private WorkersViewModel GetWorker(int workerId)
        {
            return Mapper.Map<WorkersViewModel>(_repo.GetSingle(workerId));
        }
    }
}

