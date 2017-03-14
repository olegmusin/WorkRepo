using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using ShiftsSchedule.Data;

namespace ShiftsSchedule.Models.Repository
{
    public class ShiftsRepository : GenericRepository<Shift>
    {
        private ShiftsScheduleContext _context;
        private ILogger<ShiftsRepository> _logger;

        public ShiftsRepository(ShiftsScheduleContext context, ILogger<ShiftsRepository> logger) : base(context, logger)
        {
            _logger = logger;
            _context = context;
        }

        public Shift GetSingle(int shiftId)
        {
            var query = GetAll()
                .Include(s => s.ReqSpecialties)
                .FirstOrDefault(x => x.Id == shiftId);
            return query;
        }

        public IEnumerable<Shift> GetAllForProject(int projectId)
        {
            return _context.Shifts
                .Include(s => s.ReqSpecialties)
                .Include(s => s.Project)
                .Where(p => p.Project.Id == projectId);

        }

        public IEnumerable<Shift> GetAllWithWorkers()
        {
            return GetAll()
                .Include(s => s.ReqSpecialties)
                .Include(s => s.Workers);
        }
        public IEnumerable<Shift> GetAllForWorker(int workerId)
        {
            return _context.Shifts
                .Include(s => s.ReqSpecialties)
                .Include(s => s.Workers)
                .ThenInclude(ws => ws.Worker)
                .Where(w => w.Id == workerId); 
        }

        public IEnumerable<Worker> WorkersAttendingShift(int shiftId)
        {
            var shift = _context.Shifts
                .Include(s => s.ReqSpecialties)
                .Include(s => s.Workers)
                .ThenInclude(ws => ws.Worker)
                .Single(s => s.Id == shiftId);
            return shift.Workers.Select(ws => ws.Worker);
        }

    }
}

