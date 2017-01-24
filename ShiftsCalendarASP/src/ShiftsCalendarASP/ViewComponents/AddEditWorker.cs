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
    public class AddEditWorker: ViewComponent
    {

        private readonly WorkersRepository _repo;

        public AddEditWorker(WorkersRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke(int? workerId, string mode)
        {
            ViewBag.Mode = mode;

            if (workerId != null)
            {
                var worker = GetWorker((int)workerId);
                
                return View(worker);
            }
            else
                return View();
        }

        private WorkersViewModel GetWorker(int workerId)
        {
            return  Mapper.Map<WorkersViewModel>(_repo.GetSingle(workerId));
        }
    }
}

