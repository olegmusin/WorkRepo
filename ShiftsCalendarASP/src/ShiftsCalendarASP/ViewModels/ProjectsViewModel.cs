using ShiftsCalendarASP.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShiftsCalendarASP.ViewModels
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

    }
}
