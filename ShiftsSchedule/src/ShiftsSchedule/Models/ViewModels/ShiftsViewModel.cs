﻿
using ShiftsSchedule.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShiftsSchedule.Models.ViewModels
{
    public class ShiftsViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "d")]
        public DateTime Date { get; set; }

        public ICollection<WorkerShift> Workers { get; set; } = new List<WorkerShift>();

    }
}