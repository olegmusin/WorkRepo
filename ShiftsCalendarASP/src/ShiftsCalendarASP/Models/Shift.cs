using System;
using System.Collections.Generic;

namespace ShiftsCalendarASP.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<WorkerShift> Workers { get; set; }

    }
}
