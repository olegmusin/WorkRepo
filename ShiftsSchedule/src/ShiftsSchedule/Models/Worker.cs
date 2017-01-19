using System.Collections.Generic;

namespace ShiftsSchedule.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Specialty { get; set; }
        
        public decimal Salary { get; set; }

        public virtual ICollection<WorkerShift> Shifts { get; set; }
        
    }
}
