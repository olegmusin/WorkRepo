using System;
using System.Collections.Generic;

namespace ShiftsSchedule.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Project Project { get; set; }

        public bool IsCanceled { get; set; }

        public virtual ICollection<Specialty> ReqSpecialties { get; set; }
        public virtual ICollection<WorkerShift> Workers { get; set; }

    }

}
