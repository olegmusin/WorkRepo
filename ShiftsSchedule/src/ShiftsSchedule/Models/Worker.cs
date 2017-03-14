using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShiftsSchedule.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        public string  Name { get; set; }
        public Specialty Specialty { get; set; }
        public decimal Salary { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ICollection<WorkerShift> Shifts { get; set; }
        
    }
}
