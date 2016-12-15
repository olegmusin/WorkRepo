using ShiftsCalendar.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShiftsCalendar.ViewModels
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

    }
}
