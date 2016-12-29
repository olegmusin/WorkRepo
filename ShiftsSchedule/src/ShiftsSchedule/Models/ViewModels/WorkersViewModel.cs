using Microsoft.AspNetCore.Mvc;
using ShiftsSchedule.Controllers;
using ShiftsSchedule.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShiftsSchedule.Models.ViewModels
{
    public class WorkersViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public string Specialty { get; set; }
        [Required]
        [Range(typeof(decimal),"0", "999999", ErrorMessage = "Salary must be greater than 0")]
        [DisplayFormat(DataFormatString = "$ *.*")]
        public decimal Salary { get; set; }

        public ICollection<WorkerShift> Shifts { get; set; }


        public string Action
        {
            get
            {
                Expression<Func<WorkersController, Task<IActionResult>>> update = (c => c.Update(this));
                Expression<Func<WorkersController, Task<IActionResult>>> add = (c => c.Add(this));
                var action = (Id != 0) ? update : add ;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}
