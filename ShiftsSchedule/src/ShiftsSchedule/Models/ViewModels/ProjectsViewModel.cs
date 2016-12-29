using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Controllers;
using ShiftsSchedule.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShiftsSchedule.Models.ViewModels
{
    public class ProjectsViewModel
    { 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public List<Shift> Shifts { get; set; }
        [Required]
        [Range(typeof(int), "0", "99999")]
        [Display(Name = "Workers required:")]
        public int NumberOfWorkers { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ProjectsController, Task<IActionResult>>> update = (c => c.Update(this));
                Expression<Func<ProjectsController, Task<IActionResult>>> add = (c => c.Add(this));
                var action = (Id != 0) ? update : add;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

    }
}
