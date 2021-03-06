﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShiftsSchedule.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsCanceled { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
        public int NumberOfWorkers { get; set; }
    }
}
