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
        public bool IsCanceled { get; set; }
        public ICollection<Specialty> ReqSpecialties { get; set; } = new List<Specialty>();
        public ICollection<Worker> Workers { get; set; } = new List<Worker>();

        public Project Project { get; set; }

    }
}