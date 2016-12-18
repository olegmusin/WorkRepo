namespace ShiftsCalendarASP.Models
{
    public class WorkerShift
    {
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }

        public int ShiftId { get; set; }
        public Shift Shift { get; set; }
    }
}
