using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Models.Repository;
using ShiftsSchedule.Models.ViewModels;

namespace ShiftsSchedule.ViewComponents
{
    public class AddShift : ViewComponent
    {
        private readonly ProjectsRepository _repo;
        public AddShift(ProjectsRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            return View(new ShiftsViewModel { Project = _repo.GetSingle(projectId) });
        }
    }
}
